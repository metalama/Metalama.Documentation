// This is public domain Metalama sample code.

#if TEST_OPTIONS
// @IncludeAllSeverities
#endif

namespace Doc.LAMA0043.Error;

// The [Log] aspect suggests a code fix for each method, carried by LAMA0043.
[Log]
internal class AccountService
{
    private decimal _balance = 100;

    public void Withdraw( decimal amount ) => this._balance -= amount;

    public decimal GetBalance() => this._balance;
}
