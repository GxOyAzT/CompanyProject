using ClassLibrary.ClassesModels;
using ClassLibrary.DatabaseConnections;
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
using ClassLibrary.Schedule;
using ClassLibrary.ModelsSchedule;
using System.Linq;

namespace WpfHR.PagesShedule
{
    /// <summary>
    /// Interaction logic for PageManageShedule.xaml
    /// </summary>
    public partial class PageManageShedule : Page
    {
        DateTime SelectedDate { get; set; } = DateTime.Today;
        List<MonthlySummaryModel> MonthlySummaryModels { get; set; }
        List<ScheduleModel> ScheduleModels { get; set; }
        public PageManageShedule()
        {
            InitializeComponent();
            RefreshDay();
        }

        void RefreshDay()
        {
            LblDate.Content = SelectedDate.ToString("dd-MM-yyyy");
            Calendar.SelectedDate = SelectedDate;
            ListShedule.ItemsSource = ScheduleModels = ScheduleDbConn.LoadScheduleModelsOnDate(SelectedDate);
            MonthlySummaryModels = new List<MonthlySummaryModel>();
            
            EmploymentDbConn.LoadAllEmployeesNotWorkingOnDate(SelectedDate).ForEach(e => MonthlySummaryModels.Add(new MonthlySummaryModel(e, ScheduleDbConn.SelectSchModelOnEmpAndDate(e.EmpId, new DateTime(SelectedDate.Year, SelectedDate.Month, 1)))));
            MonthlySummaryModels = (from msm in MonthlySummaryModels
                                    orderby msm.MsmEmpModel.EmpProfessionModel.ProName
                                    select msm).ToList();
            ListEmployee.ItemsSource = MonthlySummaryModels;
            ListShedule.Items.Refresh();
        }

        private void Click_Save(object sender, RoutedEventArgs e)
        {
            if(ListEmployee.SelectedIndex != -1 & (Rbt1422.IsChecked == true | Rbt614.IsChecked == true | Rbt816.IsChecked == true))
            {
                ScheduleModel newScheduleModel = new ScheduleModel(MonthlySummaryModels[ListEmployee.SelectedIndex].MsmEmpModel.EmpId, SelectedDate,
                    ReturnStartTime(), returnEndTime());
                ScheduleDbConn.InsertIntoSchedule(newScheduleModel);
                RefreshDay();
            }
        }

        private void DoubleClick_Calendar(object sender, MouseButtonEventArgs e)
        {
            SelectedDate = Convert.ToDateTime(Calendar.SelectedDate);
            RefreshDay();
        }

        private void DoubleClick_Person(object sender, MouseButtonEventArgs e)
        {

        }

        private void Click_PreviousDay(object sender, RoutedEventArgs e)
        {
            if (SelectedDate != null)
            {
                SelectedDate = SelectedDate.AddDays(-1);
                RefreshDay();
            }
        }

        private void Click_NextDay(object sender, RoutedEventArgs e)
        {
            if (SelectedDate != null)
            {
                SelectedDate = SelectedDate.AddDays(1);
                RefreshDay();
            }
        }

        TimeSpan ReturnStartTime()
        {
            if(Rbt614.IsChecked == true)
            {
                return new TimeSpan(6,0,0);
            }
            else if(Rbt1422.IsChecked == true)
            {
                return new TimeSpan(14, 0, 0);
            }
            else
            {
                return new TimeSpan(8, 0, 0);
            }
        }

        TimeSpan returnEndTime()
        {
            if (Rbt614.IsChecked == true)
            {
                return new TimeSpan(14, 0, 0);
            }
            else if (Rbt1422.IsChecked == true)
            {
                return new TimeSpan(22, 0, 0);
            }
            else
            {
                return new TimeSpan(16, 0, 0);
            }
        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            if(ListShedule.SelectedIndex != -1)
            {
                ScheduleDbConn.DeleteShiftFromSchedule(ScheduleModels[ListShedule.SelectedIndex].SchId);
                RefreshDay();
            }
        }
    }
}
