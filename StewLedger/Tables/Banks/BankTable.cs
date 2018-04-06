using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Utils;
using System.Data.SQLite;
using System.Data;

namespace StewLedger.Tables.Banks
{
    public class BankTable
    {
        

        public SQLiteCommand AddBank(string name, string phone, string routingNumber, string city, string state, int zip,
            string owner, int userId)
        {
            SQLiteCommand cmd = new SQLiteCommand();

            cmd.Parameters.Add("@name", DbType.String).Value = name;
            cmd.Parameters.Add("@phone", DbType.String).Value = phone;
            cmd.Parameters.Add("@routingNumber", DbType.String).Value = routingNumber;
            cmd.Parameters.Add("@city", DbType.String).Value = city;
            cmd.Parameters.Add("@state", DbType.String).Value = state;
            cmd.Parameters.Add("@zip", DbType.Int32).Value = zip;
            cmd.Parameters.Add("@userId", DbType.Int32).Value = userId;
            

            cmd.CommandText = "INSERT INTO "+owner+"MasterBankTable(BT_NAME, BT_PHONENUMBER, BT_ROUTINGNUMBER, BT_CITY, BT_STATE, BT_ZIP, MUT_ID) "
                + " VALUES (@name,@phone,@routingNumber,@city,@state, @zip, @userId)"; 
               

            return cmd;
        }

        public SQLiteCommand AddBankTable(string owner)
        {
            SQLiteCommand cmd = new SQLiteCommand
            {
                CommandText = "CREATE TABLE " + owner + "MasterBankTable(BT_ID INTEGER PRIMARY KEY, BT_NAME TEXT NOT NULL, BT_PHONENUMBER TEXT NOT NULL," +
                "BT_ROUTINGNUMBER TEXT NOT NULL, BT_CITY TEXT NOT NULL, BT_STATE TEXT NOT NULL, BT_ZIP INTEGER NOT NULL, MUT_ID INTEGER REFERENCES MasterUserTable(MUT_ID) ON DELETE CASCADE)"
            };

            return cmd;
        }

        public SQLiteCommand DropTable(string owner)
        {
            SQLiteCommand cmd = new SQLiteCommand
            {
                CommandText = "DROP TABLE " + owner + "MasterBankTable"
            };

            return cmd;
        }

        public SQLiteCommand GetBanks(string owner)
        {
            SQLiteCommand cmd = new SQLiteCommand
            {
                CommandText = "SELECT * FROM " + owner + "MasterBankTable"
            };

            return cmd;

        }

    }
}
