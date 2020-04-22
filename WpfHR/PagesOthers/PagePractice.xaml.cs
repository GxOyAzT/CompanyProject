using ClassLibrary.DatabaseConnections;
using ClassLibrary.ModelsSchedule;
using ClassLibrary.Schedule;
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
using ClassLibrary.ClassesModels;

namespace WpfHR.PagesShedule
{
    /// <summary>
    /// Interaction logic for PagePractice.xaml
    /// </summary>
    public partial class PagePractice : Page
    {
        DateTime SelectedDate { get; set; } = new DateTime(2020,4,1);
        List<MonthlySummaryModel> MonthlySummaryModels { get; set; }
        List<ScheduleModel> ScheduleModels { get; set; }
        List<EmployeeModel> EmployeeModels { get; set; }
        public PagePractice()
        {
            InitializeComponent();
            List<ProfessionModel> professionModels = EmploymentDbConn.LoadAllProfessions();
            //MonthlySummaryModels = new List<MonthlySummaryModel>();

            //EmploymentDbConn.LoadAllEmployeesNotWorkingOnDate(SelectedDate).ForEach(e => MonthlySummaryModels.Add(new MonthlySummaryModel(e, ScheduleDbConn.SelectSchModelOnEmpAndDate(e.EmpId, new DateTime(SelectedDate.Year, SelectedDate.Month, 1)))));
            //var xddd =  from msm in MonthlySummaryModels
            //                        group msm by msm.MsmEmpModel.EmpProfessionModel.ProName into groupModel
            //                        orderby groupModel.Key
            //                        select groupModel;
            ////ListEmployee.ItemsSource = xddd;

            //List<IGrouping<string, MonthlySummaryModel>> monthlySummaryModels = (from msm in MonthlySummaryModels
            //                                                                     group msm by msm.MsmEmpModel.EmpProfessionModel.ProName into newGroup
            //                                                                     orderby newGroup.Key
            //                                                                     select newGroup).ToList();
            //ListEmployee.ItemsSource = monthlySummaryModels;
            //ListEmployee.Items.Refresh();
            var schedulemods = ScheduleDbConn.LoadScheduleModelsOnDate(SelectedDate);
            var c = ScheduleDbConn.LoadScheduleModelsOnDate(SelectedDate).Select(sch => new EmployeeModel(sch.SchEmployeeModel)).Distinct();
            MonthlySummaryModels = ScheduleDbConn.LoadScheduleModelsOnDate(SelectedDate).Select(sch => new EmployeeModel(sch.SchEmployeeModel)).Distinct()
                .Select(emp => new MonthlySummaryModel(emp, ScheduleDbConn.SelectAllSchModels().Where(emp_sch => emp_sch.SchEmployeeModel.EmpId == emp.EmpId).ToList())).ToList();

            EmployeeModels = EmploymentDbConn.LoadAllEmployees();
            var GroupedEmployees = EmployeeModels.GroupBy(emp => emp.EmpProfessionModel.ProName);
            ListEmployee.ItemsSource = GroupedEmployees;

        }

        private void DoubleClick_Person(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
