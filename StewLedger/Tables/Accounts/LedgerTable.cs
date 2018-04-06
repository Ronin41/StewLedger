/*Author: Michael Stewart
 *Date:1/11/18 
 *Notes: Add Categories column 
 * 
 * 
 * 
 * 
 */


using Database.Utils;
using StewLedger.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StewLedger.Tables.Accounts
{
    public class LedgerTable
    {
        public LedgerTable()
        { }

        public SQLiteCommand AddTransact(string name, double amount, string comments, 
            DateTime date, string transType, string catType,int acctId, string owner, bool auto, DateTime autoDate, string acctName)
        {
            SQLiteCommand cmd = new SQLiteCommand();

            cmd.Parameters.Add("@name", DbType.String).Value = name;
            cmd.Parameters.Add("@amount", DbType.Double).Value = amount;
            cmd.Parameters.Add("@comments", DbType.String).Value = comments;
            cmd.Parameters.Add("@dates", DbType.Date).Value = date.ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@transType", DbType.String).Value = transType;
            cmd.Parameters.Add("@catType", DbType.String).Value = catType;
            cmd.Parameters.Add("@auto", DbType.String).Value = auto;
            cmd.Parameters.Add("@autodates", DbType.Date).Value = autoDate.ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@acctId", DbType.Int32).Value = acctId;
            

            cmd.CommandText = "INSERT INTO " + owner + acctName+ "Ledger(LED_NAME, LED_DATE, LED_TRANSTYPE, LED_COMMENTS, LED_AMOUNT, LED_CATTYPE, LED_AUTO, LED_AUTO_DATE, ACCT_ID) " +
                "VALUES(@name, @dates, @transType, @comments, @amount, @catType, @auto, @autodates, @acctId)";

            return cmd;
        }

        private SQLiteCommand UpdateLedgerTableSQL(string comments, double amount, string owner, string acctName, int id)
        {
            SQLiteCommand cmd = new SQLiteCommand();

            cmd.Parameters.Add("@id", DbType.Int32).Value = id;
            cmd.Parameters.Add("@amount", DbType.Int32).Value = amount;
            cmd.Parameters.Add("@comments", DbType.String).Value = comments;

            cmd.CommandText = "UPDATE " + owner + acctName +"Ledger SET LED_COMMENTS = @comments, LED_AMOUNT = @amount WHERE LED_ID = @id";

            return cmd;
        }

        private SQLiteCommand UpdateAuto(bool auto, string owner, string acctName, int id)
        {
            SQLiteCommand cmd = new SQLiteCommand();

            cmd.CommandText = "UPDATE " + owner + acctName + "Ledger SET LED_AUTO='" + auto.ToString() + "' WHERE LED_ID=" + id;

            return cmd;
        }

        private SQLiteCommand UpdateAutoTransactionSQL(bool auto, DateTime date, int amount, string acctName, string owner, int id)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Parameters.Add("@id", DbType.Int32).Value = id;
            cmd.Parameters.Add("@auto", DbType.String).Value = auto;
            cmd.Parameters.Add("@date", DbType.Date).Value = date.ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@amount", DbType.Int32).Value = amount;

            cmd.CommandText = "UPDATE " + owner + acctName + "Ledger SET LED_AUTO = @auto, LED_AUTO_DATE = @date, LED_AMOUNT = @amount WHERE LED_ID=" + id;

            return cmd;

        }



        public SQLiteCommand DeleteTransact(string owner, string acctName, int id)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Parameters.Add("@id", DbType.Int32).Value = id;

            cmd.CommandText = "DELETE FROM " + owner+acctName + "Ledger WHERE LED_ID = @id";

            return cmd;
        }
       public SQLiteCommand DropTable(string owner, string accName)
        {
            SQLiteCommand cmd = new SQLiteCommand
            {
                CommandText = "DROP TABLE " + owner + accName + "Ledger"
            };

            return cmd;
        }

        public SQLiteCommand AddLedgerTable(string owner, string acctName)
        {
            SQLiteCommand cmd = new SQLiteCommand
            {
                CommandText = "CREATE TABLE " + owner + acctName + "Ledger(LED_ID INTEGER PRIMARY KEY, LED_NAME TEXT NOT NULL, " +
                "LED_DATE DATE NOT NULL, LED_TRANSTYPE TEXT NOT NULL, LED_COMMENTS TEXT, LED_AMOUNT REAL NOT NULL, LED_CATTYPE TEXT NOT NULL," +
                "LED_AUTO TEXT, LED_AUTO_DATE DATE, ACCT_ID INTEGER REFERENCES " + owner + "Accounts(ACCT_ID) ON DELETE CASCADE)"
            };

            return cmd;
        }

        public SQLiteCommand GetLedger(string owner, string acctName)
        {

            SQLiteCommand cmd = new SQLiteCommand
            {
                CommandText = "SELECT * FROM " + owner + acctName + "Ledger"
            };

            return cmd;
        }


        public void UpdateTransactAuto(bool auto, string owner, string acctName, int id)
        {
            using (Connection conn = new Connection(ConnectionString.connectionString))
            {
                conn.NonQueryCommand(UpdateAuto(auto, owner, acctName, id));
            }
        }

        public void UpdateTransactAuto(bool auto, DateTime date, int amount, string acctName, string owner, int id)
        {
            using (Connection conn = new Connection(ConnectionString.connectionString))
            {
                conn.NonQueryCommand(UpdateAutoTransactionSQL(auto, date, amount, owner, acctName, id));
            }
        }

        public void UpdateLedgerTable(string comments, double amount, string owner, string acctName, int id)
        {
            using (Connection conn = new Connection(ConnectionString.connectionString))
            {
                conn.NonQueryCommand(UpdateLedgerTableSQL(comments, amount, owner, acctName, id));
            }
        }
    }
}
