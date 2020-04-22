using System;
using System.Windows.Controls;
using ClassLibrary.DatabaseConnections;

namespace WpfHR.PagesShedule
{
    /// <summary>
    /// Interaction logic for PageYearSalarySummary.xaml
    /// </summary>
    public partial class PageYearSalarySummary : Page
    {
        int Year { get; set; } = DateTime.Now.Year;
        public PageYearSalarySummary()
        {
            InitializeComponent();
            RefreshData();
        }
        void RefreshData()
        {
            LblYear.Content = $"{Year}";
            dtgYearSubMod.ItemsSource = ScheduleDbConn.SelectYearSummarySalaryModelByYear(Year);
            dtgYearSubMod.Items.Refresh();
        }

        private void Click_Back(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Year > 1990)
            {
                Year--;
                RefreshData();
            }
        }

        private void Click_Next(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Year < 2200) 
            { 
                Year++; 
                RefreshData(); 
            }
        }
    }
}
