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
using System.Linq;

namespace WpfHR.PagesEmployment
{
    /// <summary>
    /// Interaction logic for PageEmpManageProfesions.xaml
    /// </summary>
    public partial class PageEmpManageProfessions : Page
    {
        List<ProfessionModel> ProfessionModels { get; set; }
        public PageEmpManageProfessions()
        {
            InitializeComponent();
            RefreshData();
        }
        
        public void RefreshData()
        {
            ProfessionModels = (from profModel in EmploymentDbConn.LoadAllProfessions()
                                orderby profModel.ProName
                                select profModel).ToList();
            ListProfessions.ItemsSource = ProfessionModels;
            ListProfessions.Items.Refresh();
        }

        private void DoubleClick_Profession(object sender, MouseButtonEventArgs e)
        {
            if(ListProfessions.SelectedIndex != -1)
            {
                FrameMain.Content = new PageEmpProfession(ProfessionModels[ListProfessions.SelectedIndex], this);
            }
        }
    }
}
