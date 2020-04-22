using ClassLibrary.ClassesModels;
using ClassLibrary.Others;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ClassLibrary.DatabaseConnections
{
    public static class EmploymentDbConn
    {
        static SqlConnection conn = new SqlConnection("Server = localhost; Integrated security = SSPI; database=Company");

        // PROFESSIONS
        public static void InsertNewProfession(ProfessionModel newProfModel)
        {
            string insertNewProfession = $"INSERT INTO EmploymentProfessionInfo(EmpProName, EmpProSalary) VALUES('{newProfModel.ProName}', {newProfModel.ProSalary});";
            SqlCommand command = new SqlCommand(insertNewProfession, conn);
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void UpdateProfession(ProfessionModel updateProfModel)
        {
            string updateProfession = $"UPDATE EmploymentProfessionInfo SET " +
                $"EmpProName = '{updateProfModel.ProName}', EmpProSalary =  {updateProfModel.ProSalary} WHERE EmpProId = {updateProfModel.ProId};";
            SqlCommand command = new SqlCommand(updateProfession, conn);
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static ProfessionModel FindProfessionModelById(int id)
        {
            ProfessionModel professionModel = new ProfessionModel();
            string findById = $"SELECT * FROM EmploymentProfessionInfo WHERE EmpProId = {id}";
            SqlCommand command = new SqlCommand(findById, conn);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                professionModel = new ProfessionModel(Convert.ToInt32(reader["EmpProId"]), reader["EmpProName"].ToString(), Convert.ToInt32(reader["EmpProSalary"])); 
            }
            return professionModel;
        }
        public static List<ProfessionModel> LoadAllProfessions()
        {
            List<ProfessionModel> professionModels = new List<ProfessionModel>();
            string loadAllProfessions = $"SELECT * FROM EmploymentProfessionInfo";
            SqlCommand command = new SqlCommand(loadAllProfessions, conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                professionModels.Add(new ProfessionModel(
                    Convert.ToInt32(reader["EmpProId"]), 
                    reader["EmpProName"].ToString(), 
                    Convert.ToInt32(reader["EmpProSalary"])));
            }
            conn.Close();
            return professionModels;
        }

        // MANAGEMENTS
        public static void InsertNewManagement(ManagementModel managementModel)
        {
            string insertNewManagement = $"INSERT INTO EmploymentManagementInfo(EmpManName, EmpManSalaryIncrease) " +
                $"VALUES('{managementModel.ManName}', {managementModel.ManSalaryIncr});";
            SqlCommand command = new SqlCommand(insertNewManagement, conn);
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void UpdateManagement(ManagementModel updateManagement)
        {
            string updateProfession = $"UPDATE EmploymentManagementInfo SET " +
                $"EmpManName = '{updateManagement.ManName}', EmpManSalaryIncrease =  {updateManagement.ManSalaryIncr} WHERE EmpManId = {updateManagement.ManId};";
            SqlCommand command = new SqlCommand(updateProfession, conn);
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static ManagementModel FindManagementModelById(int id)
        {
            ManagementModel managementModel = new ManagementModel();
            string findById = $"SELECT * FROM EmploymentManagementInfo WHERE EmpManId = {id}";
            SqlCommand command = new SqlCommand(findById, conn);

            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                managementModel = new ManagementModel(Convert.ToInt32(reader["EmpManId"]), reader["EmpManName"].ToString(), Convert.ToInt32(reader["EmpManSalaryIncrease"]));
            }
            conn.Close();

            return managementModel;
        }
        public static List<ManagementModel> LoadAllManagements()
        {
            List<ManagementModel> professionModels = new List<ManagementModel>();
            string loadAllManagements = $"SELECT * FROM EmploymentManagementInfo";
            SqlCommand command = new SqlCommand(loadAllManagements, conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                professionModels.Add(new ManagementModel(
                    Convert.ToInt32(reader["EmpManId"]),
                    reader["EmpManName"].ToString(),
                    Convert.ToInt32(reader["EmpManSalaryIncrease"])));
            }
            conn.Close();
            return professionModels;
        }

        // EMPLOYEE
        public static void InsertNewEmployee(int personId, int managmentId, int professionId, string employmentDate, int time)
        {
            string insertNewEmployee = $"INSERT INTO EmployeeInfo(Emp_PerBasId, Emp_EmpProId, Emp_EmpManId, EmpDateOfEmployment, EmpTime) " +
                $"VALUES ({personId}, {professionId}, {managmentId}, '{employmentDate}', {time}); INSERT INTO EmployeeAccount (EmpAcc_EmpId, EmpAccPassword) " +
                $"VALUES((SELECT EmpId FROM EmployeeInfo WHERE Emp_PerBasId = {personId}), '{OtherFunctions.GenerateRandomPassword(8)}'); ";
            SqlCommand command = new SqlCommand(insertNewEmployee, conn);

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static List<EmployeeModel> LoadAllEmployees()
        {
            List<EmployeeModel> employeeModels = new List<EmployeeModel>();
            string insertNewEmployee = $"SELECT EmpId, EmpTime, EmpDateOfEmployment, PerBasId, PerBasFirstName, PerBasLastName, EmpManId, " +
                $"EmpManName, EmpManSalaryIncrease,EmpProId, EmpProName, EmpProSalary FROM EmployeeInfo INNER JOIN PersonBasicInfo " +
                $"ON PerBasId = Emp_PerBasId INNER JOIN EmploymentManagementInfo ON Emp_EmpManId = EmpManId INNER JOIN EmploymentProfessionInfo " +
                $"ON Emp_EmpProId = EmpProId";
            SqlCommand command = new SqlCommand(insertNewEmployee, conn);

            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                employeeModels.Add(new EmployeeModel(
                    Convert.ToInt32(reader["EmpId"]),
                    Convert.ToDateTime(reader["EmpDateOfEmployment"]),
                    Convert.ToInt32(reader["EmpTime"]),
                    new PersonModel(Convert.ToInt32(reader["PerBasId"]), 
                    reader["PerBasFirstName"].ToString(), 
                    reader["PerBasLastName"].ToString()),
                    new ManagementModel(Convert.ToInt32(reader["EmpManId"]),  reader["EmpManName"].ToString(), Convert.ToInt32(reader["EmpManSalaryIncrease"])),
                    new ProfessionModel(Convert.ToInt32(reader["EmpProId"]), reader["EmpProName"].ToString(), Convert.ToInt32(reader["EmpProSalary"]))));
            }
            conn.Close();

            return employeeModels;
        }
        public static List<EmployeeModel> LoadAllEmployeesNotWorkingOnDate(DateTime date)
        {
            List<EmployeeModel> employeeModels = new List<EmployeeModel>();
            string insertNewEmployee = $"SELECT EmpId, EmpTime, EmpDateOfEmployment, PerBasId, PerBasFirstName, PerBasLastName, EmpManId, " +
                $"EmpManName, EmpManSalaryIncrease,EmpProId, EmpProName, EmpProSalary FROM EmployeeInfo INNER JOIN PersonBasicInfo " +
                $"ON PerBasId = Emp_PerBasId INNER JOIN EmploymentManagementInfo ON Emp_EmpManId = EmpManId INNER JOIN EmploymentProfessionInfo " +
                $"ON Emp_EmpProId = EmpProId " +
                $"WHERE EmpId NOT IN (SELECT Sch_EmpId FROM Schedule WHERE SchDate = '{date.ToString("yyyy-MM-dd")}');";
            SqlCommand command = new SqlCommand(insertNewEmployee, conn);

            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                employeeModels.Add(new EmployeeModel(
                    Convert.ToInt32(reader["EmpId"]),
                    Convert.ToDateTime(reader["EmpDateOfEmployment"]),
                    Convert.ToInt32(reader["EmpTime"]),
                    new PersonModel(Convert.ToInt32(reader["PerBasId"]),
                    reader["PerBasFirstName"].ToString(),
                    reader["PerBasLastName"].ToString()),
                    new ManagementModel(Convert.ToInt32(reader["EmpManId"]), reader["EmpManName"].ToString(), Convert.ToInt32(reader["EmpManSalaryIncrease"])),
                    new ProfessionModel(Convert.ToInt32(reader["EmpProId"]), reader["EmpProName"].ToString(), Convert.ToInt32(reader["EmpProSalary"]))));
            }
            conn.Close();

            return employeeModels;
        }
        public static List<EmployeeModel> LoadFullEmployeesInfo()
        {
            List<EmployeeModel> employeeModels = new List<EmployeeModel>();
            string insertNewEmployee = $"SELECT EmpId, EmpTime, EmpDateOfEmployment, PerBasId, PerBasFirstName, PerBasLastName, EmpManId, " +
                $"EmpManName, EmpManSalaryIncrease,EmpProId, EmpProName, EmpProSalary, PerBasGender, PerBasDob, PerConEmail, PerConPhoneNumber, " +
                $"PerAdrCountry, PerAdrCity, PerAdrStreet, PerAdrZipCode " +
                $"FROM EmployeeInfo INNER JOIN PersonBasicInfo " +
                $"ON PerBasId = Emp_PerBasId INNER JOIN EmploymentManagementInfo ON Emp_EmpManId = EmpManId INNER JOIN EmploymentProfessionInfo " +
                $"ON Emp_EmpProId = EmpProId INNER JOIN PersonContactInfo ON PerCon_PerBasId = Emp_PerBasId INNER JOIN PersonAdressInfo ON PerAdr_PerBasId = Emp_PerBasId";
            SqlCommand command = new SqlCommand(insertNewEmployee, conn);

            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                employeeModels.Add(new EmployeeModel(
                    Convert.ToInt32(reader["EmpId"]),
                    Convert.ToDateTime(reader["EmpDateOfEmployment"]),
                    Convert.ToInt32(reader["EmpTime"]),
                    new PersonModel(Convert.ToInt32(reader["PerBasId"]),
                    reader["PerBasFirstName"].ToString(),
                    reader["PerBasLastName"].ToString(),
                    Convert.ToChar(reader["PerBasGender"]),
                    Convert.ToDateTime(reader["PerBasDob"]),
                    reader["PerConEmail"].ToString(),
                    reader["PerConPhoneNumber"].ToString(),
                    reader["PerAdrCountry"].ToString(),
                    reader["PerAdrCity"].ToString(),
                    reader["PerAdrStreet"].ToString(),
                    reader["PerAdrZipCode"].ToString()),
                    new ManagementModel(Convert.ToInt32(reader["EmpManId"]), reader["EmpManName"].ToString(), Convert.ToInt32(reader["EmpManSalaryIncrease"])),
                    new ProfessionModel(Convert.ToInt32(reader["EmpProId"]), reader["EmpProName"].ToString(), Convert.ToInt32(reader["EmpProSalary"]))));
            }
            conn.Close();

            return employeeModels;
        }
        public static EmployeeModel LoadFullEmployeeInfoOnId(int id)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            string insertNewEmployee = $"SELECT EmpId, EmpTime, EmpDateOfEmployment, PerBasId, PerBasFirstName, PerBasLastName, EmpManId, " +
                $"EmpManName, EmpManSalaryIncrease,EmpProId, EmpProName, EmpProSalary, PerBasGender, PerBasDob, PerConEmail, PerConPhoneNumber, " +
                $"PerAdrCountry, PerAdrCity, PerAdrStreet, PerAdrZipCode " +
                $"FROM EmployeeInfo INNER JOIN PersonBasicInfo " +
                $"ON PerBasId = Emp_PerBasId INNER JOIN EmploymentManagementInfo ON Emp_EmpManId = EmpManId INNER JOIN EmploymentProfessionInfo " +
                $"ON Emp_EmpProId = EmpProId INNER JOIN PersonContactInfo ON PerCon_PerBasId = Emp_PerBasId INNER JOIN PersonAdressInfo ON PerAdr_PerBasId = Emp_PerBasId " +
                $"WHERE EmpId = {id}";
            SqlCommand command = new SqlCommand(insertNewEmployee, conn);

            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                employeeModel = new EmployeeModel(
                    Convert.ToInt32(reader["EmpId"]),
                    Convert.ToDateTime(reader["EmpDateOfEmployment"]),
                    Convert.ToInt32(reader["EmpTime"]),
                    new PersonModel(
                        Convert.ToInt32(reader["PerBasId"]),
                        reader["PerBasFirstName"].ToString(),
                        reader["PerBasLastName"].ToString(),
                        Convert.ToChar(reader["PerBasGender"]),
                        Convert.ToDateTime(reader["PerBasDob"]),
                        reader["PerConEmail"].ToString(),
                        reader["PerConPhoneNumber"].ToString(),
                        reader["PerAdrCountry"].ToString(),
                        reader["PerAdrCity"].ToString(),
                        reader["PerAdrStreet"].ToString(),
                        reader["PerAdrZipCode"].ToString()),
                    new ManagementModel(
                        Convert.ToInt32(reader["EmpManId"]), 
                        reader["EmpManName"].ToString(), 
                        Convert.ToInt32(reader["EmpManSalaryIncrease"])),
                    new ProfessionModel(
                        Convert.ToInt32(reader["EmpProId"]), 
                        reader["EmpProName"].ToString(), 
                        Convert.ToInt32(reader["EmpProSalary"])));
            }
            conn.Close();

            return employeeModel;
        }
        public static void UpdateEmployeeManagement(int manId, int empId)
        {
            string update = $"UPDATE EmployeeInfo SET Emp_EmpManId = {manId} WHERE EmpId = {empId};";
            SqlCommand command = new SqlCommand(update, conn);

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }
    }
}
