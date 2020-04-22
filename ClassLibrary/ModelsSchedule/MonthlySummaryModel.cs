using ClassLibrary.ClassesModels;
using ClassLibrary.Schedule;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.ModelsSchedule
{
    public class MonthlySummaryModel
    {
        public EmployeeModel MsmEmpModel { get; private set; }
        public List<ScheduleModel> MsmScheduleModel { get; private set; }
        public double MsmFullHours { get
            {
                double tmp = 0;
                foreach (ScheduleModel model in MsmScheduleModel)
                {
                    tmp += (model.SchEnd - model.SchStart).TotalMinutes / 60;
                }
                return tmp;
            } }
        public double MsmSalaryGross { get => Math.Round(MsmFullHours * MsmEmpModel.EmpProfessionModel.ProSalary * MsmEmpModel.EmpManagementModel.ManSalaryIncr / 100 / 160, 2); }
        public string MsmSalaryGrossString { get => $"{MsmSalaryGross} $"; }
        public double MsmSalaryNet { get => Math.Round(MsmSalaryGross * 0.77, 2); }
        public string MsmSalaryNetString { get => $"{MsmSalaryNet} $"; }
        public MonthlySummaryModel (EmployeeModel employeeModel, List<ScheduleModel> scheduleModels)
        {
            MsmEmpModel = employeeModel;
            MsmScheduleModel = scheduleModels;
        }
    }
}
