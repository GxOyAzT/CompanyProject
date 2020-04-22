using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.ClassesModels
{
    public class EmployeeModel
    {
        public int EmpId { get; private set; } = -1;
        public DateTime EmpDateOfEmployment { get; private set; }
        public int EmpTime { get; private set; }
        public ManagementModel EmpManagementModel { get; private set; }
        public ProfessionModel EmpProfessionModel { get; private set; }
        public PersonModel EmpPersonModel { get; private set; }
        public string EmpDateOfEmploymentString { get => EmpDateOfEmployment.ToString("dd-MM-yyyy"); }
        public string EmpTimeString
        {
            get
            {
                if (EmpTime == 1) return "Full (160)";
                else if (EmpTime == 2) return "Half (80)";
                else return "Quoter (40)";
            }
        }
        public string EmpSalaryGross { get => $"{Math.Round(EmpProfessionModel.ProSalary * EmpManagementModel.ManSalaryIncr / 100 / EmpTime * 1.0, 2)} $"; }
        public string EmpSalaryNet { get => $"{Math.Round(EmpProfessionModel.ProSalary * EmpManagementModel.ManSalaryIncr / 100 / EmpTime * 0.77, 2)} $"; }
        public string EmpExperience { get => $"{Math.Floor((DateTime.Now - EmpDateOfEmployment).Days / 355.25)}"; }
        public string EmpProAndMan { get => $"{EmpProfessionModel.ProName} ({EmpManagementModel.ManName})"; }

        public EmployeeModel(int empId)
        {
            EmpId = empId;
        }
        public EmployeeModel()
        {
            EmpId = -1;
        }
        public EmployeeModel(int empId, string perFirstName, string perLastName, int empManId, int empProId)
        {
            EmpId = empId;
            EmpPersonModel = new PersonModel(perFirstName, perLastName);
            EmpProfessionModel = new ProfessionModel(empProId);
            EmpManagementModel = new ManagementModel(empManId);
        }
        public EmployeeModel(int empId, DateTime employmentDate, int empTime, PersonModel personModel, ManagementModel managementModel, ProfessionModel professionModel)
        {
            EmpId = empId;
            EmpDateOfEmployment = employmentDate;
            EmpManagementModel = managementModel;
            EmpProfessionModel = professionModel;
            EmpPersonModel = personModel;
            EmpTime = empTime;
        }
        public EmployeeModel(PersonModel personModel, ManagementModel managementModel, ProfessionModel professionModel)
        {
            EmpPersonModel = personModel;
            EmpManagementModel = managementModel;
            EmpProfessionModel = professionModel;
        }
        public EmployeeModel(int empId, PersonModel personModel, ManagementModel managementModel, ProfessionModel professionModel)
        {
            EmpId = empId;
            EmpPersonModel = personModel;
            EmpManagementModel = managementModel;
            EmpProfessionModel = professionModel;
        }
        public EmployeeModel(int empId, PersonModel personModel)
        {
            EmpId = empId;
            EmpPersonModel = personModel;
        }
        public EmployeeModel(EmployeeModel emp)
        {
            EmpId = emp.EmpId;
            EmpDateOfEmployment = emp.EmpDateOfEmployment;
            EmpTime = emp.EmpTime;
            EmpManagementModel = emp.EmpManagementModel;
            EmpProfessionModel = emp.EmpProfessionModel;
            EmpPersonModel = emp.EmpPersonModel;
        }

        
    }

    public class EmployeeComparer : IEqualityComparer<EmployeeModel>
    {
        public bool Equals(EmployeeModel x, EmployeeModel y)
        {
            return x.EmpId == y.EmpId;
        }

        public int GetHashCode(EmployeeModel obj)
        {
            return obj.EmpId;
        }
    }
}
