using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StewLedger.Tables.Users
{
    public class UserTable
    {
        
        public SQLiteCommand AddUser(string firstName, string lastName, string password, string username, string tableName)
        {
            SQLiteCommand cmd = new SQLiteCommand();

            cmd.Parameters.Add("@firstName", DbType.String).Value = firstName;
            cmd.Parameters.Add("@lastName", DbType.String).Value = lastName;
            cmd.Parameters.Add("@password", DbType.String).Value = password;
            cmd.Parameters.Add("@username", DbType.String).Value = username;
            

            cmd.CommandText = "INSERT INTO "+tableName+"(MUT_FIRSTNAME, MUT_LASTNAME, MUT_PASSWORD, MUT_USERNAME) " +
                "VALUES (@firstName,@lastName,@password,@username)";

            return cmd;
        }

        public SQLiteCommand AddUserTable()
        {
            SQLiteCommand cmd = new SQLiteCommand();

            cmd.CommandText = "CREATE TABLE MasterUserTable(MUT_ID INTEGER PRIMARY KEY, MUT_FIRSTNAME TEXT NOT NULL, MUT_LASTNAME TEXT NOT NULL, MUT_PASSWORD TEXT NOT NULL, " +
                "MUT_USERNAME TEXT UNIQUE NOT NULL)";

            return cmd;
        }

        public SQLiteCommand FindAccount(string username)
        {
            SQLiteCommand cmd = new SQLiteCommand();

            cmd.Parameters.Add("@username", DbType.String).Value = username;

            cmd.CommandText =  "SELECT * FROM MasterUserTable WHERE MUT_USERNAME = @username";

            return cmd;
        }

        public SQLiteCommand DeleteAccount(int acct_id)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "DELETE FROM MasterUserTable WHERE MUT_ID="+acct_id;

            return cmd;

        }
    }
}
