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
    /// Interaction logic for PageEmpManagement.xaml
    /// </summary>
    public partial class PageEmpManagement : Page
    {
        ManagementModel ManagementModel { get; set; }
        PageEmpManageManagements PageEmpManageManagements { get; set; }
        bool isNewCreating { get; set; }
        public PageEmpManagement(ManagementModel? managementModel, PageEmpManageManagements? pageManageManagments)
        {
            InitializeComponent();
            if (managementModel != null)
            {
                isNewCreating = false;
                PageEmpManageManagements = pageManageManagments;
                ManagementModel = managementModel;
                TxbManagementName.Text = ManagementModel.ManName;
                TxbManagementSalaryIncr.Text = ManagementModel.ManSalaryIncr.ToString();
            }
            else isNewCreating = true;
        }

        private void Click_Save(object sender, RoutedEventArgs e)
        {
            if (isNewCreating)
            {
                ManagementModel newManagementModel = new ManagementModel(TxbManagementName.Text, Convert.ToInt32(TxbManagementSalaryIncr.Text));
                EmploymentDbConn.InsertNewManagement(newManagementModel);
                TxbManagementName.Text = null;
                TxbManagementSalaryIncr.Text = null;
            }
            else
            {
                ManagementModel updateManagementModel = new ManagementModel(ManagementModel.ManId, TxbManagementName.Text, Convert.ToInt32(TxbManagementSalaryIncr.Text));
                EmploymentDbConn.UpdateManagement(updateManagementModel);
                PageEmpManageManagements.RefreshData();
            }
        }
    }
}
