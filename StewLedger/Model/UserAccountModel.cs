using StewLedger.Util;


namespace StewLedger.Model
{
    public class UserAccountModel : Notifier
    {
        private string _first, _last, _password, _username;

        public string First
        {
            get { return _first; }
            set { _first = value; OnPropertyChanged("First"); }
        }
        public string Last
        {
            get { return _last; }
            set { _last = value; OnPropertyChanged("Last"); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged("Password"); }
        }

        public string UserName
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged("UserName"); }
        }
    }
}





