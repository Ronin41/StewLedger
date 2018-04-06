using StewLedger.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StewLedger.Interfaces
{
    interface IAccountParticipant
    {
        void SetAccount(Accounts accounts);
        event EventHandler AccountChanged;
        Accounts GetAccounts();

    }
}
