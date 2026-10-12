"""Rewrites the **Message** row of each article from the source message in artifacts/diagnostics/inventory.json.

Run it after the corrected messages are merged into the Metalama sources and the inventory is regenerated from them.
The placeholder names of the current row are kept: the row is matched against the message it was written for (the
proposal of MessageReview/LAMA####.json when the verdict is 'correct', else the previous source message passed with
--previous), which tells which `<Name>` stands for which `{n}`. Rows that can't be matched are reported, not changed.

Usage: python Update-MessageRows.py --previous artifacts/diagnostics/inventory-before.json [--dry-run]
"""
import argparse
import json
import pathlib
import re

root = pathlib.Path(__file__).resolve().parents[4]
parser = argparse.ArgumentParser()
parser.add_argument('--previous', required=True)
parser.add_argument('--dry-run', action='store_true')
args = parser.parse_args()


def load(path):
    return {e['Id']: e for e in json.loads(pathlib.Path(path).read_text(encoding='utf-8-sig'))}


current = load(root / 'artifacts/diagnostics/inventory.json')
previous = load(root / args.previous)
reviews = root / 'code/Metalama.Documentation.SampleCode.Errors/MessageReview'
row_regex = re.compile(r'(?m)^(\|\s*\*\*Message\*\*\s*\|\s*)(`+) ?(.*?) ?\2(\s*\|\s*)$')
placeholder = re.compile(r'\{(\d+)(?::[^}]*)?\}')

changed, unmatched = [], []
for article in sorted((root / 'content/errors').glob('lama*.md')):
    diag = article.stem.upper()
    if diag not in current or not current[diag].get('Message'):
        continue
    new_message = current[diag]['Message'].strip()
    review_path = reviews / f'{diag}.json'
    review = json.loads(review_path.read_text(encoding='utf-8-sig')) if review_path.exists() else {}
    old_message = (review.get('message') if review.get('verdict') == 'correct' else None) \
        or (previous.get(diag) or {}).get('Message') or new_message
    old_message = old_message.strip()

    raw = article.read_bytes()
    text = raw.decode('utf-8-sig')
    rows = list(row_regex.finditer(text))
    if len(rows) != 1:
        continue  # Several definitions share this ID: leave the rows to a human.
    row = rows[0]

    # Learn which name each placeholder has in the current row.
    pattern = '^'
    order = []
    last = 0
    for m in placeholder.finditer(old_message):
        pattern += re.escape(old_message[last:m.start()]) + r'<([A-Za-z][A-Za-z0-9]*)>'
        order.append(m.group(1))
        last = m.end()
    pattern += re.escape(old_message[last:]) + '$'
    match = re.match(pattern, row.group(3).strip())
    if not match:
        unmatched.append(diag)
        continue
    names = dict(zip(order, match.groups()))

    missing = sorted({m.group(1) for m in placeholder.finditer(new_message)} - names.keys())
    if missing:
        unmatched.append(f'{diag} (no name for {{{",".join(missing)}}})')
        continue
    rendered = placeholder.sub(lambda m: f'<{names[m.group(1)]}>', new_message)
    if rendered == row.group(3).strip():
        continue

    ticks = '``' if '`' in rendered else '`'
    pad = ' ' if ticks == '``' else ''
    new_text = text[:row.start()] + f'{row.group(1)}{ticks}{pad}{rendered}{pad}{ticks}{row.group(4)}' + text[row.end():]
    changed.append(diag)
    if not args.dry_run:
        article.write_bytes((b'\xef\xbb\xbf' if raw.startswith(b'\xef\xbb\xbf') else b'') + new_text.encode('utf-8'))

print(f'{len(changed)} rows updated{" (dry run)" if args.dry_run else ""}: {" ".join(changed)}')
print(f'{len(unmatched)} rows not matched: {" ".join(unmatched)}')
