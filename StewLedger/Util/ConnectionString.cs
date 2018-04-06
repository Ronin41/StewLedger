using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StewLedger.Util
{
    /// <summary>
    /// Singleton Class Thread Safe
    /// </summary>
    class ConnectionString
    {
        public static string connectionString;
        public static readonly ConnectionString instance = new ConnectionString(connectionString);

        public ConnectionString(string connectionString1)
        {
            connectionString = connectionString1;
        }

        public static ConnectionString Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
