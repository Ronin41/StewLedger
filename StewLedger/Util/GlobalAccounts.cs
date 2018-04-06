using StewLedger.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StewLedger.Util
{
    public sealed class GlobalAccounts
    {
        public static Accounts _accounts;

        public static readonly GlobalAccounts instance = new GlobalAccounts(_accounts);

        public GlobalAccounts(Accounts accounts)
        {
            _accounts = accounts;
        }

        public static GlobalAccounts Instance => instance;
    }
}
