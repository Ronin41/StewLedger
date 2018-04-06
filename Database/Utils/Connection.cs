using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Win32.SafeHandles;

namespace Database.Utils
{
    public class Connection : IDisposable
    {
       
        public SQLiteConnection conn = new SQLiteConnection();
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        System.Runtime.InteropServices.SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        private string _connectionString;


        public Connection()
        {
            //conn = new SQLiteConnection(@"data source=|DataDirectory|StewLedgerDB.db;Foreign Keys=True");
            
        }

        public Connection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool NonQueryCommand(SQLiteCommand cmd)
        {
            try
            {

                using (conn = new SQLiteConnection(_connectionString))
                {

                    //SQLiteCommand cmd = new SQLiteCommand();
                    conn.Open();

                    using (cmd.Connection = conn)
                    {
                        // cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                }
            }
            catch(Exception e)
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (Directory.Exists(folder + "/StewLedger/Data") == false)
                {
                    Directory.CreateDirectory(folder + "/StewLedger/Data");
                    FileHandler fh = new FileHandler(folder + "/StewLedger/Data", "ErrorLog.txt");
                    fh.Write(e.Message);
                }
                else
                {
                    FileHandler fh = new FileHandler(folder + "/StewLedger/Data", "ErrorLog.txt");
                    fh.Write(e.Message);
                }


              
                return false;
            }
        }

        public List<string> QueryCommand(SQLiteCommand cmd)
        {

            List<string> list = new List<string>(); 

            try
            {

                using (conn = new SQLiteConnection(_connectionString))
                { 
                    
                    conn.Open();
                    using (cmd.Connection = this.conn)
                    {
                        SQLiteDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            for (int i = 0; i < rdr.FieldCount; i++)
                            {
                                list.Add(rdr[i].ToString());
                            }

                        }

                        rdr.Close();
                        conn.Close();

                        return list;
                    }
                }
            }catch(Exception e)
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (Directory.Exists(folder + "/StewLedger/Data") == false)
                {
                    Directory.CreateDirectory(folder + "/StewLedger/Data");
                    FileHandler fh = new FileHandler(folder + "/StewLedger/Data", "Error.txt");
                    fh.Write(e.Message);
                }
                else
                {
                    FileHandler fh = new FileHandler(folder + "/StewLedger/Data", "Error.txt");
                    fh.Write(e.Message);
                }
                
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return null;
            }
        }

        public bool TableExist(string tableName)
        {

            try
            {
                using (conn = new SQLiteConnection(_connectionString))
                {
                    conn.Open();

                    SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM sqlite_master WHERE type='table' AND name='" + tableName + "'", conn);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    bool exists = reader.Read();
                    reader.Close();
                    conn.Close();
                    return exists;
                }
            }
            catch (Exception e)
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (Directory.Exists(folder + "/StewLedger/Data") == false)
                {
                    Directory.CreateDirectory(folder + "/StewLedger/Data");
                    FileHandler fh = new FileHandler(folder + "/StewLedger/Data", "Error.txt");
                    fh.Write(e.Message);
                }
                else
                {
                    FileHandler fh = new FileHandler(folder + "/StewLedger/Data", "Error.txt");
                    fh.Write(e.Message);
                }
            }
            conn.Close();
            return false;
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
               
            }

            disposed = true;
        }
    }
}
