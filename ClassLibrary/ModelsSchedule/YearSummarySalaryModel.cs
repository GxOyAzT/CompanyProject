using ClassLibrary.ClassesModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ClassLibrary.ModelsSchedule
{
    public class YearSummarySalaryModel
    {
        public EmployeeModel YsmEmployee { get; private set; }
        public string YsmEmpFullName { get; set; }
        public double [] YsmMonths { get; private set; }
        public string YsmJan { get => $"{YsmMonths[0]:C2}"; }
        public string YsmFeb { get => $"{YsmMonths[1]:C2}"; }
        public string YsmMar { get => $"{YsmMonths[2]:C2}"; }
        public string YsmApr { get => $"{YsmMonths[3]:C2}"; }
        public string YsmMay { get => $"{YsmMonths[4]:C2}"; }
        public string YsmJun { get => $"{YsmMonths[5]:C2}"; }
        public string YsmJul { get => $"{YsmMonths[6]:C2}"; }
        public string YsmAug { get => $"{YsmMonths[7]:C2}"; }
        public string YsmSep { get => $"{YsmMonths[8]:C2}"; }
        public string YsmOct { get => $"{YsmMonths[9]:C2}"; }
        public string YsmNov { get => $"{YsmMonths[10]:C2}"; }
        public string YsmDec { get => $"{YsmMonths[11]:C2}"; }
        public string YsmYearSum { get => $"{YsmMonths.Sum():C2}"; }

        public YearSummarySalaryModel (EmployeeModel emp, double[] months)
        {
            YsmEmployee = emp;
            YsmMonths = months;
        }

        public YearSummarySalaryModel(string empFullName, double[] months)
        {
            YsmEmpFullName = empFullName;
            YsmMonths = months;
        }
    }
}
