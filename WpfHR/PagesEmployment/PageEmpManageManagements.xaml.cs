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
    /// Interaction logic for PageEmpManageManagements.xaml
    /// </summary>
    public partial class PageEmpManageManagements : Page
    {
        List<ManagementModel> ManagementModels { get; set; }
        public PageEmpManageManagements()
        {
            InitializeComponent();
            RefreshData();
        }
        public void RefreshData()
        {
            ManagementModels = EmploymentDbConn.LoadAllManagements();
            ListManagements.ItemsSource = ManagementModels;
            ListManagements.Items.Refresh();
        }

        private void DoubleClick_Management(object sender, MouseButtonEventArgs e)
        {
            if(ListManagements.SelectedIndex != -1)
            {
                FrameMain.Content = new PageEmpManagement(ManagementModels[ListManagements.SelectedIndex], this);
            }
        }
    }
}
