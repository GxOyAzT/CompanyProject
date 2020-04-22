using ClassLibrary.ClassesModels;
using System;
using System.Threading;
using System.Windows;
using WpfHR.LogIn;
using WpfHR.Pages;
using WpfHR.PagesEmployment;
using WpfHR.PagesOthers;
using WpfHR.PagesShedule;
using WpfHR.ToDoc;

namespace WpfHR
{
    public partial class MainWindow : Window
    {
        public bool isLogIn = true;
        EmployeeModel LoggedInUser { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Clock();
            //isLogIn = false;
        }

        // MENU LOG IN
        private void ClickMenu_LogIn(object sender, RoutedEventArgs e)
        {
            if (!isLogIn)
            {
                FrameMain.Content = new PageLogin(this);
            }
        }
        private void ClickMenu_LogOut(object sender, RoutedEventArgs e)
        {
            if (isLogIn)
            {
                isLogIn = false;
                LoggedInUser = new EmployeeModel();
                TblUser.Text = "Successfully loged out.";
                FrameMain.Content = new PageBlank();
            }
        }

        private void Click_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //      MENU ITEMS EMPLOYEE
        private void ClickPersonal_AddNew(object sender, RoutedEventArgs e)
        {
            if (isLogIn)
            {
                FrameMain.Content = new PagePersonalData(null);
            }
            else MessageBox.Show("You have to log in to use this method");
        }
        private void ClickPersonal_Manage(object sender, RoutedEventArgs e)
        {
            if (isLogIn)
            {
                FrameMain.Content = new PageManagePersonalData(); 
            }
            else MessageBox.Show("You have to log in to use this method");
        }
        private void ClickPersonal_SeeAll(object sender, RoutedEventArgs e)
        {
            if (isLogIn)
            {
                FrameMain.Content = new PagePeopleTable(); 
            }
            else MessageBox.Show("You have to log in to use this method");
        }

        private void ClickEmployment_AddProfession(object sender, RoutedEventArgs e)
        {
            if (isLogIn)
            {
                FrameMain.Content = new PageEmpProfession(null, null); 
            }
            else MessageBox.Show("You have to log in to use this method");
        }
        private void ClickEmployment_ManageProfessions(object sender, RoutedEventArgs e)
        {
            if (isLogIn)
            {
                FrameMain.Content = new PageEmpManageProfessions(); 
            }
            else MessageBox.Show("You have to log in to use this method");
        }
        private void ClickEmployment_AddManagement(object sender, RoutedEventArgs e)
        {
            if (isLogIn)
            {
                FrameMain.Content = new PageEmpManagement(null, null); 
            }
            else MessageBox.Show("You have to log in to use this method");
        }
        private void ClickEmployment_ManageManagements(object sender, RoutedEventArgs e)
        {
            if (isLogIn)
            {
                FrameMain.Content = new PageEmpManageManagements(); 
            }
            else MessageBox.Show("You have to log in to use this method");
        }
        private void ClickEmployment_AddEmployee(object sender, RoutedEventArgs e)
        {
            if (isLogIn)
            {
                FrameMain.Content = new PageEmpEmployee(); 
            }
            else MessageBox.Show("You have to log in to use this method");
        }

        private void Click_Minimise(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ClickEmployment_TableEmployees(object sender, RoutedEventArgs e)
        {
            if (isLogIn)
            {
                FrameMain.Content = new PageEmpAllEmployeeTable(this);
            }
            else MessageBox.Show("You have to log in to use this method");
        }

        private void ClickEmployment_ManageEmployees(object sender, RoutedEventArgs e)
        {
            if (isLogIn)
            {
                FrameMain.Content = new PageManageEmployee();
            }
            else MessageBox.Show("You have to log in to use this method");
        }

        private void ClickSchedule_ManageShedule(object sender, RoutedEventArgs e)
        {
            if (isLogIn)
            {
                FrameMain.Content = new PageManageShedule();
            }
            else MessageBox.Show("You have to log in to use this method");
        }

        private void ClickSchedule_MonthlySummary(object sender, RoutedEventArgs e)
        {
            if (isLogIn)
            {
                FrameMain.Content = new PageMonthlySummary();
            }
            else MessageBox.Show("You have to log in to use this method");
        }

        private void Clock()
        {
            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    try
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            this.LblTime.Content = DateTime.Now.ToString("hh:mm:ss");
                        });
                    }
                    catch
                    {
                        break;
                    }
                }
            }).Start();
        }

        private void ClickMenu_Practice(object sender, RoutedEventArgs e)
        {
            FrameMain.Content = new PagePractice();
        }

        private void ClickSchedule_YearSalarySummary(object sender, RoutedEventArgs e)
        {
            if (isLogIn)
            {
                FrameMain.Content = new PageYearSalarySummary();
            }
            else MessageBox.Show("You have to log in to use this method");
        }
    }
}
