using System;


namespace StewLedger.Util
{
    public class AccountsNotifier
    {
        public event EventHandler accountsChanged;
        public void OnAccountsChanged(object sender, EventArgs eventArgs)
        {
            accountsChanged?.Invoke(sender, eventArgs);
        }
    }

    public class AccountNotifier
    {
        public event EventHandler accountChanged;

        public void OnAccountChanged(object sender, EventArgs eventArgs)
        {
            accountChanged?.Invoke(sender, eventArgs);
        }
    }
}
