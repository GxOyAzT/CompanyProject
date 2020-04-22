using ClassLibrary.ClassesModels;
using ClassLibrary.DatabaseConnections;
using System.Windows;
using System.Windows.Controls;
using WpfHR.PagesOthers;

namespace WpfHR.LogIn
{
    /// <summary>
    /// Interaction logic for PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        MainWindow MainWindow { get; set; }
        public PageLogin(MainWindow mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;
        }

        private void Click_BtnLogIn(object sender, System.Windows.RoutedEventArgs e)
        {
            EmployeeModel LogInEmpModel = LoginDbConn.LogInHumanResources(TxbEmail.Text, PasswordBox.Password.ToString());
            if (LogInEmpModel.EmpId != -1)
            {
                MainWindow.TblUser.Text = $"Log in: {LogInEmpModel.EmpPersonModel.PerFullName}";
                MainWindow.FrameMain.Content = new PageBlank();
                MainWindow.isLogIn = true;
            }
            else MessageBox.Show("Email or password is incorrect or \nyou have no premission to log into HR.");
        }
    }
}
