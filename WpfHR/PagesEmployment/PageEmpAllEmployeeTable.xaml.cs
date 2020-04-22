using ClassLibrary.ClassesModels;
using ClassLibrary.DatabaseConnections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
using Word = Microsoft.Office.Interop.Word;

namespace WpfHR.PagesEmployment
{
    /// <summary>
    /// Interaction logic for PageEmpAllEmployeeTable.xaml
    /// </summary>
    public partial class PageEmpAllEmployeeTable : Page
    {
        MainWindow MainWindow { get; set; }
        List<EmployeeModel> EmployeeModels { get; set; }
        public PageEmpAllEmployeeTable(MainWindow? mainWindow)
        {
            if(mainWindow != null)
            {
                MainWindow = mainWindow;
            }
            InitializeComponent();
            EmployeeModels = EmploymentDbConn.LoadFullEmployeesInfo();
            RefreshData();
        }
        private void RefreshData()
        {
            DtgEmployees.ItemsSource = EmployeeModels;
        }

        private void Click_CreateDocContract(object sender, RoutedEventArgs e)
        {
            if (DtgEmployees.SelectedIndex != -1)
            {
                MainWindow.LblLoading.Visibility = Visibility.Visible;
                CreateWordDocument();
                MainWindow.LblLoading.Visibility = Visibility.Hidden;
            }
            else MessageBox.Show("Choose employee first.");
        }
        public void FindAndReplace(Word.Application wordApp, object ToFindText, object replaceWithText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundLike = false;
            object nmatchAllforms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiactitics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;

            wordApp.Selection.Find.Execute(ref ToFindText,
                ref matchCase, ref matchWholeWord,
                ref matchWildCards, ref matchSoundLike,
                ref nmatchAllforms, ref forward,
                ref wrap, ref format, ref replaceWithText,
                ref replace, ref matchKashida,
                ref matchDiactitics, ref matchAlefHamza,
                ref matchControl);
        }

        //Creeate the Doc Method
        public void CreateWordDocument()
        {
            Word.Application wordApp = new Word.Application();
            object missing = Missing.Value;
            Word.Document myWordDoc = null;
            object filename = @"D:\C#Programs\CompanyProject\WpfHR\docs\EmploymentContract\EmploymentContract.docx";

            if (File.Exists((string)filename))
            {
                object readOnly = false;
                object isVisible = false;
                wordApp.Visible = false;

                myWordDoc = wordApp.Documents.Open(ref filename, ref missing, ref readOnly,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing, ref missing);
                myWordDoc.Activate();

                //find and replace
                this.FindAndReplace(wordApp, "<dateOfEmployment>", $"{DateTime.Now.ToString("dd-MM-yyyy")}");
                this.FindAndReplace(wordApp, "<firstName>", $"{EmployeeModels[DtgEmployees.SelectedIndex].EmpPersonModel.PerFirstName}");
                this.FindAndReplace(wordApp, "<lastName>", $"{EmployeeModels[DtgEmployees.SelectedIndex].EmpPersonModel.PerLastName}");
                this.FindAndReplace(wordApp, "<dob>", $"{EmployeeModels[DtgEmployees.SelectedIndex].EmpPersonModel.PerDob.ToString("dd-MM-yyyy")}");
                this.FindAndReplace(wordApp, "<gender>", $"{EmployeeModels[DtgEmployees.SelectedIndex].EmpPersonModel.PerGender}");
                this.FindAndReplace(wordApp, "<phoneNumber>", $"{EmployeeModels[DtgEmployees.SelectedIndex].EmpPersonModel.PerContactModel.PerPhone}");
                this.FindAndReplace(wordApp, "<email>", $"{EmployeeModels[DtgEmployees.SelectedIndex].EmpPersonModel.PerContactModel.PerEmail}");
                this.FindAndReplace(wordApp, "<country>", $"{EmployeeModels[DtgEmployees.SelectedIndex].EmpPersonModel.PerAdressModel.PerAdrCountry}");
                this.FindAndReplace(wordApp, "<city>", $"{EmployeeModels[DtgEmployees.SelectedIndex].EmpPersonModel.PerAdressModel.PerAdrCity}");
                this.FindAndReplace(wordApp, "<street>", $"{EmployeeModels[DtgEmployees.SelectedIndex].EmpPersonModel.PerAdressModel.PerAdrStreet}");
                this.FindAndReplace(wordApp, "<zipCode>", $"{EmployeeModels[DtgEmployees.SelectedIndex].EmpPersonModel.PerAdressModel.PerAdrZipCode}");
                this.FindAndReplace(wordApp, "<profession>", $"{EmployeeModels[DtgEmployees.SelectedIndex].EmpProfessionModel.ProName}");
                this.FindAndReplace(wordApp, "<management>", $"{EmployeeModels[DtgEmployees.SelectedIndex].EmpManagementModel.ManName}");
                this.FindAndReplace(wordApp, "<grossSalary>", $"{EmployeeModels[DtgEmployees.SelectedIndex].EmpSalaryGross}");
                this.FindAndReplace(wordApp, "<netSalary>", $"{EmployeeModels[DtgEmployees.SelectedIndex].EmpSalaryNet}");



            }
            else
            {
                MessageBox.Show("File not Found!");
            }

            //Save as
            myWordDoc.SaveAs(@$"D:\C#Programs\CompanyProject\WpfHR\docs\EmploymentContract\EmploymentContracts\{EmployeeModels[DtgEmployees.SelectedIndex].EmpPersonModel.PerFirstName}{EmployeeModels[DtgEmployees.SelectedIndex].EmpPersonModel.PerLastName}EC.docx", ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing);

            myWordDoc.Close();
            wordApp.Quit();
            MessageBox.Show("File Created!");
        }
    }
}
