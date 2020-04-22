using ClassLibrary;
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

namespace WpfHR
{
    /// <summary>
    /// Interaction logic for PageEmployeeData.xaml
    /// </summary>
    public partial class PagePersonalData : Page
    {
        PersonModel? CurrentPerson { get; set; }
        PersonModel PersonDataUserInput { get; set; }
        bool IsNewPersonPage { get; set; }
        public PagePersonalData(PersonModel? personInfo)
        {
            InitializeComponent();
            if (personInfo != null)
            {
                CurrentPerson = personInfo;
                IsNewPersonPage = false;
                InitializeData();
                CmbGender.IsReadOnly = true;
            }
            else IsNewPersonPage = true;
        }
        private void InitializeData()
        {
            // PERSONAL DATA
            TxbFirstName.Text = CurrentPerson.PerFirstName;
            TxbLastName.Text = CurrentPerson.PerLastName;
            TxbDob.Text = CurrentPerson.PerDob.ToString("yyyy-MM-dd");
            if (CurrentPerson.PerGender == 'M')
                CmbGender.SelectedIndex = 0;
            else
                CmbGender.SelectedIndex = 1;

            // CONTACT DATA
            TxbEmail.Text = CurrentPerson.PerContactModel.PerEmail;
            TxbPhoneNumber.Text = CurrentPerson.PerContactModel.PerPhone;

            // CONTENT ADRESS
            TxbCountry.Text = CurrentPerson.PerAdressModel.PerAdrCountry;
            TxbCity.Text = CurrentPerson.PerAdressModel.PerAdrCity;
            TxbStreet.Text = CurrentPerson.PerAdressModel.PerAdrStreet;
            TxbZipCode.Text = CurrentPerson.PerAdressModel.PerAdrZipCode;
        }
        private char GetGender()
        {
            if (CmbGender.SelectedIndex == 1) return 'M';
            else return 'F';
        }
        private void Click_AddEmployee(object sender, RoutedEventArgs e)
        {
            if (IsNewPersonPage)
            {
                PersonDataUserInput = new PersonModel(TxbFirstName.Text, TxbLastName.Text, GetGender(), Convert.ToDateTime(TxbDob.Text), TxbEmail.Text,
                    TxbPhoneNumber.Text, TxbCountry.Text, TxbCity.Text, TxbStreet.Text, TxbZipCode.Text);
                PersonDbConn.InsertFullPersonInfo(PersonDataUserInput);
            }
            else
            {
                PersonDataUserInput = new PersonModel(CurrentPerson.PerId, TxbFirstName.Text, TxbLastName.Text, GetGender(), Convert.ToDateTime(TxbDob.Text), TxbEmail.Text,
                    TxbPhoneNumber.Text, TxbCountry.Text, TxbCity.Text, TxbStreet.Text, TxbZipCode.Text);
                PersonDbConn.UpdateFullPersonInfo(PersonDataUserInput);
            }
        }
    }
}
