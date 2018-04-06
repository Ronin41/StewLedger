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
    public sealed class User
    {
        public static int _userId;
        public static string _username;

        private static readonly User instance = new User(_userId, _username);


        public User(int userId, string username)
        {
            _userId = userId;
            _username = username;
        }

        public static User Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
