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
    /// Interaction logic for PageEmpProffesion.xaml
    /// </summary>
    public partial class PageEmpProfession : Page
    {
        private ProfessionModel? ProfessionModel { get; set; }
        private PageEmpManageProfessions? PageEmpManageProfessions { get; set; }
        private bool isNewCreating { get; set; }
        public PageEmpProfession(ProfessionModel? professionModel, PageEmpManageProfessions? pageEmpManageProfessions)
        {
            InitializeComponent();
            ProfessionModel = professionModel;
            if (professionModel != null)
            {
                TxbProfesionName.Text = ProfessionModel.ProName;
                TxbProfesionSalary.Text = ProfessionModel.ProSalary.ToString();
                isNewCreating = false;
                PageEmpManageProfessions = pageEmpManageProfessions;
            }
            else isNewCreating = true;
        }

        private void Click_Save(object sender, RoutedEventArgs e)
        {
            if (isNewCreating)
            {
                ProfessionModel newProfession = new ProfessionModel(TxbProfesionName.Text, Convert.ToInt32(TxbProfesionSalary.Text));
                EmploymentDbConn.InsertNewProfession(newProfession);
                TxbProfesionName.Text = null;
                TxbProfesionSalary.Text = null;
            }
            else
            {
                ProfessionModel updateProfession = new ProfessionModel(ProfessionModel.ProId, TxbProfesionName.Text, Convert.ToInt32(TxbProfesionSalary.Text));
                EmploymentDbConn.UpdateProfession(updateProfession);
                PageEmpManageProfessions.RefreshData();
            }
        }
    }
}
