"""Links every mention of a documented LAMA#### diagnostic in content/**/*.md to its article.

A mention is a bare `LAMA0208`, a code span containing only `LAMA0208`, or an existing `<xref:lama0208>`;
it becomes `<xref:lama0208?text=LAMA0208>`, because a plain xref to an article renders its whole title.

Skipped: front matter, headings, fenced code blocks, include directives, the Message rows of error articles,
the generated category pages and index table, links, other code spans, and an article's mentions of itself.

Usage: python Update-ErrorCodeLinks.py [--dry-run]
"""
import pathlib
import re
import sys

root = pathlib.Path(__file__).resolve().parents[4]
content = root / 'content'
errors = content / 'errors'
documented = {p.stem.upper() for p in errors.glob('lama*.md')}
dry_run = '--dry-run' in sys.argv

# Tokens that must be kept as they are, in order of precedence. A code span that contains only an ID is handled separately.
token = re.compile(
    r'(?P<xref><xref:[^>]*>)'
    r'|(?P<link>\[[^\]]*\]\([^)]*\))'
    r'|(?P<code>(`+)(?P<codetext>.+?)\5)'
    r'|(?P<id>\bLAMA\d{4}\b)')


def rewrite_line(line, self_id):
    def replace(m):
        if m.group('xref'):
            x = re.fullmatch(r'<xref:(lama\d{4})>', m.group('xref'))
            if x and x.group(1).upper() in documented and x.group(1).upper() != self_id:
                return f'<xref:{x.group(1)}?text={x.group(1).upper()}>'
            return m.group(0)
        if m.group('link'):
            return m.group(0)
        if m.group('code'):
            inner = m.group('codetext').strip()
            if re.fullmatch(r'LAMA\d{4}', inner) and inner in documented and inner != self_id:
                return f'<xref:{inner.lower()}?text={inner}>'
            return m.group(0)
        diag = m.group('id')
        if diag in documented and diag != self_id:
            return f'<xref:{diag.lower()}?text={diag}>'
        return m.group(0)

    return token.sub(replace, line)


changed_files = 0
changed_mentions = 0
for path in sorted(content.rglob('*.md')):
    if path.parent == errors and path.name.startswith('errors-'):
        continue  # Generated category pages.
    raw = path.read_bytes()
    bom = raw.startswith(b'\xef\xbb\xbf')
    text = raw.decode('utf-8-sig')
    newline = '\r\n' if '\r\n' in text else '\n'
    lines = text.replace('\r\n', '\n').split('\n')
    self_id = path.stem.upper() if path.parent == errors else None
    in_front_matter = lines[:1] == ['---']
    in_fence = False
    in_generated = False
    out = []
    for i, line in enumerate(lines):
        stripped = line.lstrip()
        if in_front_matter:
            out.append(line)
            if i > 0 and line == '---':
                in_front_matter = False
            continue
        if stripped.startswith('```') or stripped.startswith('~~~'):
            in_fence = not in_fence
            out.append(line)
            continue
        if 'BEGIN GENERATED' in line:
            in_generated = True
        if 'END GENERATED' in line:
            in_generated = False
        if (in_fence or in_generated or stripped.startswith('#') or stripped.startswith('[!')
                or stripped.startswith('| **Message** |')):
            out.append(line)
            continue
        out.append(rewrite_line(line, self_id))
    new_text = '\n'.join(out)
    if new_text != text.replace('\r\n', '\n'):
        mentions = sum(a != b for a, b in zip(lines, out))
        changed_files += 1
        changed_mentions += new_text.count('?text=LAMA') - text.count('?text=LAMA')
        print(f'{path.relative_to(root)}: {mentions} line(s)')
        if not dry_run:
            path.write_bytes((b'\xef\xbb\xbf' if bom else b'') + new_text.replace('\n', newline).encode('utf-8'))
print(f'{changed_files} files, {changed_mentions} links{" (dry run)" if dry_run else ""}')
