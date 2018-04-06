using StewLedger.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StewLedger.Model
{
    public class AccountCreationModel:Notifier
    {
        private string accountName;
        private string accountType;
        private double balance;
        private int bankId;
        private string accountNumber;

        public string AccountName { get => accountName; set { accountName = value; OnPropertyChanged("AccountName"); } }
        public string AccountType { get => accountType; set { accountType = value; OnPropertyChanged("AccountType"); } }
        public double Balance { get => balance; set { balance = value; OnPropertyChanged("Balance"); } }
        public int Bank { get => bankId; set { bankId = value; OnPropertyChanged("Bank"); } }
        public string AccountNumber { get => accountNumber; set { accountNumber = value; OnPropertyChanged("AccountNumber"); } }
    }
}
