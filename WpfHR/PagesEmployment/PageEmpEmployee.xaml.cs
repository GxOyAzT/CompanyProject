using ClassLibrary;
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
    /// Interaction logic for PageEmpEmployee.xaml
    /// </summary>
    public partial class PageEmpEmployee : Page
    {
        List<PersonModel> PersonModels { get; set; }
        PersonModel SelectedPersonModel { get; set; }
        List<ManagementModel> ManagementModels { get; set; }
        List<ProfessionModel> ProfessionModels { get; set; }
        public PageEmpEmployee()
        {
            InitializeComponent();
            PersonModels = PersonDbConn.GetFullPersonInfo();
            ManagementModels = EmploymentDbConn.LoadAllManagements();
            CbxManagement.ItemsSource = ManagementModels;
            ProfessionModels = EmploymentDbConn.LoadAllProfessions();
            CbxProfessions.ItemsSource = ProfessionModels;
            ReloadPersonData();
        }
        private int SelectedRadioButton()
        {
            if (RbtFull.IsChecked == true)
            {
                return 1;
            }
            else if (RbtHalf.IsChecked == true)
            {
                return 2;
            }
            else if (RbtQuoter.IsChecked == true)
            {
                return 4;
            }
            else return -1;
        }
        private void ReloadPersonData()
        {
            PersonModels = PersonDbConn.GetFullPersonInfoAreNotEmployeed();
            ListPeople.ItemsSource = PersonModels;
            ListPeople.Items.Refresh();
        }

        private void DoubleClick_Person(object sender, MouseButtonEventArgs e)
        {
            if(ListPeople.SelectedIndex != -1)
            {
                SelectedPersonModel = PersonModels[ListPeople.SelectedIndex];
                TblPerson.Text = SelectedPersonModel.PerFullName;
            }
        }

        private void Click_Save(object sender, RoutedEventArgs e)
        {
            if (TblPerson.Text != null & CbxManagement.SelectedIndex != -1 & CbxProfessions.SelectedIndex != -1 & SelectedRadioButton() != -1)
            {
                EmploymentDbConn.InsertNewEmployee(SelectedPersonModel.PerId, ManagementModels[CbxManagement.SelectedIndex].ManId,
                    ProfessionModels[CbxProfessions.SelectedIndex].ProId, ReturnDateOfEmploymentString(), SelectedRadioButton() );
                TblPerson.Text = null;
                CbxProfessions.SelectedIndex = -1;
                CbxManagement.SelectedIndex = -1;
                TxbDateOfEmp.Text = null;
                ReloadPersonData();
            }
            else MessageBox.Show("Input data is incorrect.");
        }

        string ReturnDateOfEmploymentString()
        {
            if (CbxEmpDateCurrent.IsChecked == true) return DateTime.Now.ToString("yyyy-MM-dd");
            else return TxbDateOfEmp.Text;
        }
    }
}
