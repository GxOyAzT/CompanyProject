using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.ClassesModels
{
    public class ManagementModel
    {
        public int ManId { get; set; }
        public string ManName { get; set; }
        public int ManSalaryIncr { get; set; }
        public ManagementModel(int manId, string manName, int proSalary) : this(manName, proSalary)
        {
            ManId = manId;
        }
        public ManagementModel(string manName, int manSalary)
        {
            ManName = manName;
            ManSalaryIncr = manSalary;
        }
        public ManagementModel(int manId)
        {
            ManId = manId;
        }
        public ManagementModel(string manName) => ManName = manName;
        public ManagementModel() => ManId = -1;
    }
}
