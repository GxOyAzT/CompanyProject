using ClassLibrary.ClassesModels;
using ClassLibrary.DatabaseConnections;
using ClassLibrary.ModelsSchedule;
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

namespace WpfHR.PagesShedule
{
    /// <summary>
    /// Interaction logic for PageMonthlySummary.xaml
    /// </summary>
    public partial class PageMonthlySummary : Page
    {
        DateTime SelectedMonth { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        List<MonthlySummaryModel> MonthlySummaryModels { get; set; } = new List<MonthlySummaryModel>();
        
        public PageMonthlySummary()
        {
            InitializeComponent();
            ReloadMonth();
        }

        void ReloadMonth()
        {
            MonthlySummaryModels = new List<MonthlySummaryModel>();
            LblMonth.Content = $"{SelectedMonth.Month} - {SelectedMonth.Year}";
            LoadFullInfo();
            DtgMonthlySum.ItemsSource = MonthlySummaryModels;
            DtgMonthlySum.Items.Refresh();
        }

        private void Click_Back(object sender, RoutedEventArgs e)
        {
            SelectedMonth = SelectedMonth.AddMonths(-1);
            ReloadMonth();
        }


        private void Click_Next(object sender, RoutedEventArgs e)
        {
            SelectedMonth = SelectedMonth.AddMonths(1);
            ReloadMonth();
        }

        private void LoadFullInfo()
        {
            foreach(EmployeeModel em in ScheduleDbConn.SelectDistinctEmployeeOnMonth(SelectedMonth))
                MonthlySummaryModels.Add(new MonthlySummaryModel(em, ScheduleDbConn.SelectSchModelOnEmpAndDate(em.EmpId, SelectedMonth)));
        }
    }
}
