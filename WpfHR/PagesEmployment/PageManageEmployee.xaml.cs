using ClassLibrary.ClassesModels;
using ClassLibrary.DatabaseConnections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfHR.PagesEmployment
{
    /// <summary>
    /// Interaction logic for PageManageEmployee.xaml
    /// </summary>
    public partial class PageManageEmployee : Page
    {
        List<EmployeeModel> EmployeeModels { get; set; }
        EmployeeModel SelectedEmployeeModel { get; set; } = new EmployeeModel();
        List<ManagementModel> ManagementModels { get; set; }
        public PageManageEmployee()
        {
            InitializeComponent();
            ManagementModels = EmploymentDbConn.LoadAllManagements();
            CbxManagement.ItemsSource = ManagementModels;
            RefreshData();
        }
        private void RefreshData()
        {
            EmployeeModels = EmploymentDbConn.LoadAllEmployees();
            ListEmployee.ItemsSource = EmployeeModels;
        }

        private void DoubleClick_Person(object sender, MouseButtonEventArgs e)
        {
            if(ListEmployee.SelectedIndex != -1)
            {
                SelectedEmployeeModel = EmployeeModels[ListEmployee.SelectedIndex];
                TblPerson.Text = SelectedEmployeeModel.EmpPersonModel.PerFullName;
            }
        }

        private void Click_Save(object sender, RoutedEventArgs e)
        {
            if (SelectedEmployeeModel.EmpId != -1 & CbxManagement.SelectedIndex != -1)
            {
                EmploymentDbConn.UpdateEmployeeManagement(ManagementModels[CbxManagement.SelectedIndex].ManId, SelectedEmployeeModel.EmpId);
            }
            else MessageBox.Show("Select data correctly.");
        }
    }
}
