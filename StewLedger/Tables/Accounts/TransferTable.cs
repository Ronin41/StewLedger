using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Utils;
using System.Data.SQLite;
using System.Data;
using StewLedger.Model;
using StewLedger.Util;

namespace StewLedger.Tables.Accounts
{
    /// <summary>
    /// Creates middle table to track transfers for single or table deletion
    /// </summary>
   public class TransferTable : Base.BaseTable
    {
       public override SQLiteCommand AddTable(string Owner)
        {
            SQLiteCommand cmd = new SQLiteCommand
            {
                CommandText = "CREATE TABLE "+Owner+ "TransferTable(TT_ID INTEGER PRIMARY KEY, TT_SOURCE TEXT NOT NULL, TT_DEST TEXT NOT NULL, TT_SOURCE_ACCT_ID INTEGER NOT NULL, TT_DEST_ACCT_ID INTEGER NOT NULL, " +
                "TT_SOURCE_ITEM_ID INTEGER NOT NULL, TT_DEST_ITEM_ID INTEGER NOT NULL, TT_AUTO TEXT, TT_DATE DATE NOT NULL, TT_AUTO_DATE DATE," +
                "MUT_ID INTEGER REFERENCES MasterUserTable(MUT_ID) ON DELETE CASCADE)"
            };

            return cmd;
        }

        private SQLiteCommand AddRow(string source, string dest, int sourceAcctId, 
            int destAcctId, int sourceId, int destId, int acctId, string owner, string auto, DateTime date, DateTime autoDate)
        {
            SQLiteCommand cmd = new SQLiteCommand();

            cmd.Parameters.Add("@source", DbType.String).Value = source;
            cmd.Parameters.Add("@dest", DbType.String).Value = dest;
            cmd.Parameters.Add("@destAcctId", DbType.Int32).Value = destAcctId;
            cmd.Parameters.Add("@sourceAcctId", DbType.Int32).Value = sourceAcctId;
            cmd.Parameters.Add("@destId", DbType.Int32).Value = destId;
            cmd.Parameters.Add("@sourceId", DbType.Int32).Value = sourceId;
            cmd.Parameters.Add("@acctId", DbType.Int32).Value = acctId;
            cmd.Parameters.Add("@auto", DbType.String).Value = auto;
            cmd.Parameters.Add("@date", DbType.Date).Value = date.ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@autoDate", DbType.Date).Value = autoDate.ToString("yyyy-MM-dd");


            cmd.CommandText = "INSERT INTO "+owner+ "TransferTable(TT_SOURCE, TT_DEST, TT_SOURCE_ACCT_ID, TT_DEST_ACCT_ID, " +
                "TT_SOURCE_ITEM_ID, TT_DEST_ITEM_ID, TT_AUTO, TT_DATE, TT_AUTO_DATE, MUT_ID) " +
                "VALUES (@source, @dest,@sourceAcctId, @destAcctId, @sourceId, @destId, @auto, @date, @autoDate, @acctId)";

            return cmd;
        }

        private SQLiteCommand GetRows(string owner)
        {
            SQLiteCommand cmd = new SQLiteCommand();

            cmd.CommandText = "SELECT * FROM " + owner + "TransferTable";

            return cmd;
        }

        private SQLiteCommand UpdateAuto(bool auto, string owner, int id)
        {
            SQLiteCommand cmd = new SQLiteCommand();

            cmd.CommandText = "UPDATE "+owner+"TransferTable SET TT_AUTO='"+auto.ToString()+"' WHERE TT_ID="+id;

            return cmd;
        }

        public List<string> ReturnRows(string owner)
        {
            List<string> list;

            using (Connection conn = new Connection(ConnectionString.connectionString))
            {
               list = conn.QueryCommand(GetRows(owner)); 
            }

            return list;
        }

        private SQLiteCommand Delete(string owner, int id)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Parameters.Add("@id", DbType.Int32).Value = id;

            cmd.CommandText = "DELETE FROM " + owner + "TransferTable WHERE TT_ID = @id";

            return cmd;
        }

        public void AddTransfer(string source, string dest, int sourceAcctId, int destAcctId, int sourceId, int destId, DateTime date, DateTime autoDate, int acctId, string owner, string auto)
        {
            using (Connection conn = new Connection(ConnectionString.connectionString))
            {
                conn.NonQueryCommand(AddRow(source, dest, sourceAcctId, destAcctId, sourceId, 
                    destId,acctId, owner, auto, date, autoDate));
            }
        }

        public void CreateTable(string owner)
        {
            using (Connection conn = new Connection(ConnectionString.connectionString))
            {
              conn.NonQueryCommand(AddTable(owner));
            }
        }

        public void DeleteRow(string owner, int id)
        {
            using (Connection conn = new Connection(ConnectionString.connectionString))
            {
                conn.NonQueryCommand(Delete(owner, id));
            }
        }


        public void UpdateTransferAuto(bool auto, string owner, int id)
        {
            using (Connection conn = new Connection(ConnectionString.connectionString))
            {
                conn.NonQueryCommand(UpdateAuto(auto, owner, id));
            }
        }

    }
}
