using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.ClassesModels
{
    public class ProfessionModel
    {
        public int ProId { get; set; }
        public string ProName { get; set; }
        public int ProSalary { get; set; }
        public string ProSalaryString { get => $"{ProSalary} $"; }
        public string ProSalaryNetString { get => $"{ProSalary * 0.77} $"; }
        public ProfessionModel(int proId, string proName, int proSalary) : this (proName, proSalary)
        {
            ProId = proId;
        }
        public ProfessionModel(string proName, int proSalary)
        {
            ProName = proName;
            ProSalary = proSalary;
        }
        public ProfessionModel (int proId)
        {
            ProId = proId;
        }
        public ProfessionModel(string proName) => ProName = proName;
        public ProfessionModel() => ProId = -1;
    }
}
