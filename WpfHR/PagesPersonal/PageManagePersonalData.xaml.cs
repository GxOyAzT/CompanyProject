using ClassLibrary;
using ClassLibrary.DatabaseConnections;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Windows;

namespace WpfHR.Pages
{
    /// <summary>
    /// Interaction logic for PageManagePersonalData.xaml
    /// </summary>
    public partial class PageManagePersonalData : Page
    {
        List<PersonModel> PeopleInfo { get; set; }
        List<PersonModel> PeopleInfoXX { get; set; }
        public PageManagePersonalData()
        {
            InitializeComponent();
            PeopleInfo = PersonDbConn.GetFullPersonInfo();
            PeopleInfoXX = (from person in PeopleInfo
                            orderby person.PerFullName
                            select person).ToList();
            ReloadPersonsList();
        }
        public void ReloadPersonsList()
        {
            ListPeople.ItemsSource = PeopleInfoXX;
            ListPeople.Items.Refresh();
        }

        private void DoubleClick_Person(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FrameMain.Content = new PagePersonalData(PeopleInfoXX[ListPeople.SelectedIndex]);
        }

        private void TextChanged_TxbSearch(object sender, TextChangedEventArgs e)
        {
            PeopleInfoXX = (from person in PeopleInfo
                          where person.PerFullName.Contains(TxbSearch.Text)
                          select person).ToList();
            ReloadPersonsList();
        }
    }
}
