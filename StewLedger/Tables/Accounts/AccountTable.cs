using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StewLedger.Tables.Base;

namespace StewLedger.Tables.Accounts 
{
    public class AccountTable : BaseTable
    {
        public SQLiteCommand AddAccount(string accountType, string accountName,string owner, 
            int bankId, string accountNum, double startingBalance, double accountBalance)
        {
            SQLiteCommand cmd = new SQLiteCommand();

            cmd.Parameters.Add("@accountType", DbType.String).Value = accountType;
            cmd.Parameters.Add("@accountName", DbType.String).Value = accountName;
            cmd.Parameters.Add("@accountBalance", DbType.String).Value = accountBalance;
            cmd.Parameters.Add("@bankId", DbType.String).Value = bankId;
            cmd.Parameters.Add("@accountNum", DbType.String).Value = accountNum;
            cmd.Parameters.Add("@startingBalance", DbType.String).Value = startingBalance;

            cmd.CommandText = "INSERT INTO "+owner+ "Accounts(ACCT_NAME, ACCT_TYPE, ACCT_BALANCE, ACCT_STARTBALANCE, ACC_NUM, BT_ID) " +
                "VALUES (@accountName,@accountType, @accountBalance, @startingBalance, @accountNum, @bankId)";

            return cmd;
        }

      

       override public SQLiteCommand AddTable(string owner)
        {
            SQLiteCommand cmd = new SQLiteCommand();

             cmd.CommandText = "CREATE TABLE "+owner+"Accounts(ACCT_ID INTEGER PRIMARY KEY, ACCT_NAME TEXT NOT NULL, " +
                "ACCT_TYPE TEXT NOT NULL, ACCT_STARTBALANCE REAL NOT NULL, ACCT_BALANCE, ACC_NUM TEXT NOT NULL, BT_ID INTEGER REFERENCES " + owner+"MasterBankTable(BT_ID) ON DELETE CASCADE)";

            return cmd;
        }


         override public SQLiteCommand FindRow(string owner, string tableName, int Id)
        {
            SQLiteCommand cmd = new SQLiteCommand();

            cmd.Parameters.Add("@accId", DbType.Int32).Value = Id;

            cmd.CommandText = "SELECT * FROM " + owner + tableName + " WHERE ACCT_ID = @accId";

            return cmd;
        }

        public SQLiteCommand DropTable(string owner)
        {
            SQLiteCommand cmd = new SQLiteCommand
            {
                CommandText = "DROP TABLE " + owner + "Accounts"
            };

            return cmd;
        }

        public SQLiteCommand GetAccounts(string owner)
        {
            SQLiteCommand cmd = new SQLiteCommand();

            cmd.CommandText = "SELECT * FROM " + owner+"Accounts";

            return cmd;

        }

        public SQLiteCommand UpdateBalance(string owner, double balance, int id)
        {
            SQLiteCommand cmd = new SQLiteCommand();

            cmd.Parameters.Add("@balance", DbType.Double).Value = balance;
            cmd.Parameters.Add("@id", DbType.Int32).Value = id;

            cmd.CommandText = "UPDATE "+owner+"Accounts SET ACCT_BALANCE=@balance WHERE ACCT_ID = @id";

            return cmd;
        }
    }
}
