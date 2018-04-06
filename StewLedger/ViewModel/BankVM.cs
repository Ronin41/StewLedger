using System;
using System.Collections.Generic;
using StewLedger.Util;
using StewLedger.Model;
using System.Collections.ObjectModel;
using StewLedger.Tables.Banks;
using Database.Utils;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Windows.Forms;

namespace StewLedger.ViewModel
{




   public class BankVM : Notifier, IDisposable
    {


        private Connection conn = new Connection(ConnectionString.connectionString);
        private BankTable bankTable = new BankTable();
        private bool disposed = false;
        private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        private BankList banks = new BankList();
        private BankModel bankModel;

        public BankModel BankModel { get => bankModel; set { bankModel = value; OnPropertyChanged("BankModel"); } }



        public BankVM()
        {
            this.BankModel = new BankModel();
        }

        public bool AddNewBankDB(BankModel bank)
        {
            if (conn.NonQueryCommand(bankTable.AddBank(bank.Name, bank.PhoneNumber, bank.RoutingNumber, bank.City, bank.State, bank.Zipcode, User._username, User._userId)) == true)
            {
                return true;
            }

            return false;
        }

        public void AddNewBank(BankModel bank)
        {
            bankModel = bank;
        }

        public BankList LoadBanks()
        {
            Connection connection = new Connection(ConnectionString.connectionString);
            BankTable bankTable = new BankTable();
            List<string> list = connection.QueryCommand(bankTable.GetBanks(User._username));

            int numRecords = list.Count / 8;


            int item = 0, item1 = 1, item2 = 2, item3 = 3, item4 = 4, item5 = 5, item6 = 6, item7 = 7;

            for (int i = 0; i < numRecords; i++)
            {
                BankModel bank= new BankModel();

                bank.Id = Convert.ToInt32(list[item]);
                bank.Name = list[item1];
                bank.PhoneNumber = list[item2];
                bank.RoutingNumber = list[item3];
                bank.City = list[item4];
                bank.State = list[item5];
                bank.Zipcode = Convert.ToInt32(list[item6]);
                bank.MutId = Convert.ToInt32(list[item7]);

                banks.Add(bank);

                item += 8;
                item1 += 8;
                item2 += 8;
                item3 += 8;
                item4 += 8;
                item5 += 8;
                item6 += 8;
                item7 += 8;

            }

            return banks;
        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            //Free managed resources
            if (disposing)
            {
                handle.Dispose();
                conn.Dispose();
            }

            disposed = true;
        }


        private RelayCommand<BankModel> addNewBankCmd = null;
        public RelayCommand<BankModel> AddNewBankCmd => addNewBankCmd ?? 
            (addNewBankCmd = new RelayCommand<BankModel>(AddBank, CanAddBank));
  
        private bool CanAddBank(BankModel bank) => bank != null;
        private void AddBank(BankModel bank) {

            try
            {
                AddNewBankDB(bank);
                MessageBox.Show("Bank Added", "Success", MessageBoxButtons.OK);
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK);
            }
                
                                              }

    }

    public class BankList : ObservableCollection<BankModel>
    {
        public BankList()
        {
        }
        public void AddBanks(BankModel bank)
        {
            this.Add(bank);
        }
    }

    public class StateList : ObservableCollection<string>
    {

        public StateList()
            {
            this.Add("AL");
            this.Add("AK");
            this.Add("AZ");
            this.Add("AR");
            this.Add("CA");
            this.Add("CO");
            this.Add("CT");
            this.Add("DE");
            this.Add("FL");
            this.Add("GA");
            this.Add("HI");
            this.Add("ID");
            this.Add("IL");
            this.Add("IN");
            this.Add("IA");
            this.Add("KS");
            this.Add("KY");
            this.Add("LA");
            this.Add("ME");
            this.Add("MD");
            this.Add("MA");
            this.Add("MI");
            this.Add("MN");
            this.Add("MS");
            this.Add("MO");
            this.Add("MT");
            this.Add("NE");
            this.Add("NV");
            this.Add("NH");
            this.Add("NJ");
            this.Add("NM");
            this.Add("NY");
            this.Add("NC");
            this.Add("ND");
            this.Add("OH");
            this.Add("OK");
            this.Add("OR");
            this.Add("PA");
            this.Add("RI");
            this.Add("SC");
            this.Add("SD");
            this.Add("TN");
            this.Add("TX");
            this.Add("UT");
            this.Add("VT");
            this.Add("VA");
            this.Add("WA");
            this.Add("WV");
            this.Add("WI");
            this.Add("WY");
        }

    }


}
