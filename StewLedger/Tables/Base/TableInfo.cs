namespace StewLedger.Tables.Base
{

    public partial class BaseTable
    {
        public struct TableInfo
        {
            public enum Table { ACCT, LEDGER, BANK, USER, TRANSFER};

            private string tableName;
            private int id;
            private string owner;
            private Table t;

            public string Owner { get => owner; set => owner = value; }
            public string TableName { get => tableName; set => tableName = value; }
            public int Id { get => id; set => id = value; }
            public Table T { get => t; set => t = value; }
        }

    }
}
