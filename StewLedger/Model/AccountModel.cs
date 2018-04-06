using StewLedger.Util;


namespace StewLedger.Model
{
    public class AccountModel :  Notifier
    {
        private string _accountType;
        private string _accountName;
        private string _accountNumber;
        private double _balance, _startingBalance;
        private int _Id;
        private int _bankId;

        public int Id { get => _Id; set { _Id = value; OnPropertyChanged("Id"); } }
        public string AccountType { get => _accountType; set { _accountType = value; OnPropertyChanged("AccountType"); } }
        public string AccountName { get => _accountName; set { _accountName = value; OnPropertyChanged("AccountName"); } }
        public double Balance { get => _balance; set { _balance = value; OnPropertyChanged("Balance"); } }

        public int BankId { get => _bankId; set { _bankId = value; OnPropertyChanged("BankId"); } }

        public string AccountNumber { get => _accountNumber; set { _accountNumber = value; OnPropertyChanged("AccountNumber"); } }

        public double StartingBalance { get => _startingBalance; set { _startingBalance = value; OnPropertyChanged("StartingBalance"); } }
    }

  
}


