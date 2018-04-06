using System;
using StewLedger.Util;
using System.Windows.Media;


namespace StewLedger.Model
{
    public class LedgerModel : Notifier
    {
        private int _ledgerId, _accId;
        private double _amount, _balance;
        private string _transType, _comments, _name, _catType;
        private DateTime _date, _autoDate;
        private bool _auto;

        private SolidColorBrush _color;

        public int LedgerId { get => _ledgerId; set { _ledgerId = value; OnPropertyChanged("LedgerId"); } }
        public int AccId { get => _accId; set { _accId = value; OnPropertyChanged("AccId"); } }
        public double Amount { get => _amount; set { _amount = value; OnPropertyChanged("Amount"); } }
        public string TransType { get => _transType; set { _transType = value; OnPropertyChanged("TransType"); } }
        public string Comments { get => _comments; set { _comments = value; OnPropertyChanged("Comments"); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged("Name"); } }
        public DateTime Date { get => _date; set { _date = value; OnPropertyChanged("Date"); } }

        public SolidColorBrush Color { get => _color; set { _color = value; OnPropertyChanged("IsWithdrawal"); } }
        public string CatType { get => _catType; set { _catType = value; OnPropertyChanged("CatType"); } }

        public double Balance { get => _balance; set { _balance = value; OnPropertyChanged("Balance"); } }

        public bool Auto { get => _auto; set { _auto = value; OnPropertyChanged("Auto"); } }

        public DateTime AutoDate { get => _autoDate; set { _autoDate = value; OnPropertyChanged("AutoDate"); } }
    }

    public class ExpenseBreakdownModel : Notifier
    {
        private string name;
        private double amount;

        public string Name { get => name; set { name = value; OnPropertyChanged("Name"); } }
        public double Amount { get => amount; set { amount = value; OnPropertyChanged("Amount"); } }
    }
}
