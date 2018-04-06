using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StewLedger.Tables.Base
{

    public partial class BaseTable
    {

       

    public BaseTable()
    { }
    public virtual SQLiteCommand RemoveRow(TableInfo info)
        {
            SQLiteCommand cmd;
            string tableIdAttribute = null;

            switch(info.T)
            {
                case TableInfo.Table.ACCT:
                    {
                        tableIdAttribute = "ACCT_ID";
                    }
                    break;
                case TableInfo.Table.BANK:
                    {
                        tableIdAttribute = "BT_ID";
                    }
                    break;
                case TableInfo.Table.LEDGER:
                    {
                        tableIdAttribute = "LED_ID";
                    }
                    break;
                case TableInfo.Table.USER:
                    {
                        tableIdAttribute = "MUT_ID";
                    }
                    break;
                case TableInfo.Table.TRANSFER:
                    {
                        tableIdAttribute = "TT_ID";
                    }break;
            }

            cmd = new SQLiteCommand();
            cmd.Parameters.Add("@id", DbType.Int32).Value = info.Id;
            cmd.CommandText = "DELETE FROM " + info.Owner + info.TableName + " WHERE "+tableIdAttribute+"=@id";

            return cmd;
        }

    public virtual SQLiteCommand FindRow(string x, string y, int z) { return null; }

    public virtual SQLiteCommand AddTable(string x) { return null; }

    public virtual SQLiteCommand AddRow() { return null;}
        
        

    }
}
