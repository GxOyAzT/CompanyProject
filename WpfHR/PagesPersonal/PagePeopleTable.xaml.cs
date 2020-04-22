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

namespace WpfHR.Pages
{
    /// <summary>
    /// Interaction logic for PagePopleTable.xaml
    /// </summary>
    public partial class PagePeopleTable : Page
    {
        List<PersonModel> PersonModels { get; set; }
        public PagePeopleTable()
        {
            InitializeComponent();
            PersonModels = PersonDbConn.GetFullPersonInfo();
            DtgPeople.ItemsSource = PersonModels;
        }
    }
}
