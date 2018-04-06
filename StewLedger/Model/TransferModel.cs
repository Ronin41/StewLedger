using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StewLedger.Util;

namespace StewLedger.Model
{
    public class TransferModel : Notifier
    {
        private int id;
        private string source;
        private string dest;
        private int sourceAcctId;
        private int destAcctId;
        private int sourceId;
        private int destId;
        private DateTime date;
        private DateTime autoDate;
        private string auto;

        //Transfer situations//
        private double amount;
        private string comments;

        public int Id { get => id; set { id = value; OnPropertyChanged("Id"); } }
        public string Source { get => source; set { source = value; OnPropertyChanged("Source"); } }
        public string Dest { get => dest; set { dest = value; OnPropertyChanged("Dest"); } }
        public int SourceAcctId { get => sourceAcctId; set { sourceAcctId = value; OnPropertyChanged("SourceAcctId"); } }
        public int DestAcctId { get => destAcctId; set { destAcctId = value; OnPropertyChanged("DestAcctId"); } }
        public int SourceId { get => sourceId; set { sourceId = value; OnPropertyChanged("SourceId"); } }
        public int DestId { get => destId; set { destId = value; OnPropertyChanged("DestId"); } }
        public DateTime Date { get => date; set { date = value; OnPropertyChanged("Date"); } }
        public DateTime AutoDate { get => autoDate; set { autoDate = value; OnPropertyChanged("AutoDate"); } }
        public string Auto { get => auto; set { auto = value; OnPropertyChanged("Auto"); } }

        public double Amount { get => amount; set { amount = value; OnPropertyChanged("Amount"); } }
        public string Comments { get => comments; set => comments = value; }
    }
}
