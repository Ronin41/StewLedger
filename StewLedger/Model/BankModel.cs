using StewLedger.Util;
namespace StewLedger.Model
{
    public class BankModel : Notifier
    {

        private int _id;
        private string _name;
        private string _phoneNumber;
        private string _routingNumber;
        private string _city;
        private string _state;
        private int _zipcode;
        private int _mutId;

        public string Name { get => _name; set { _name = value; OnPropertyChanged("Name"); } }

        public string PhoneNumber { get => _phoneNumber; set { _phoneNumber = value; OnPropertyChanged("PhoneNumber"); } }
        public string RoutingNumber { get => _routingNumber; set { _routingNumber = value; OnPropertyChanged("RoutingNumber"); } }
        public string City { get => _city; set { _city = value; OnPropertyChanged("City"); } }
        public string State { get => _state; set { _state = value; OnPropertyChanged("State"); } }
        public int Zipcode { get => _zipcode; set { _zipcode = value; OnPropertyChanged("Zipcode"); } }
        public int MutId { get => _mutId; set { _mutId = value; OnPropertyChanged("MutId"); } }
        public int Id { get => _id; set { _id = value; OnPropertyChanged("Id"); } }

        
    }
}
