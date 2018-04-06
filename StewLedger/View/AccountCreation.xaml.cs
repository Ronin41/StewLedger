using System.Windows;


namespace StewLedger.View
{
    /// <summary>
    /// Interaction logic for AccountCreation.xaml
    /// </summary>
    public partial class AccountCreation : Window
    {
       
        DashBoardView view;
       
        public AccountCreation()
        {
            InitializeComponent();
        }

        public void Set(ref DashBoardView view)
        {
            view = new DashBoardView();
            
                this.view = view;
                btnCreate.CommandParameter = this.view;
        }

  
    }
}
