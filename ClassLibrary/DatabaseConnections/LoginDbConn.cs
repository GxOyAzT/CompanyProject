using ClassLibrary.ClassesModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ClassLibrary.DatabaseConnections
{
    public static class LoginDbConn
    {
        static SqlConnection conn = new SqlConnection("Server = localhost; Integrated security = SSPI; database=Company");

        public static EmployeeModel LogInHumanResources(string email, string password)
        {
            
            bool IsDataCorrect = false;
            EmployeeModel LoginEmployeeModel = new EmployeeModel();
            string logInHR = $"SELECT EmpId, EmpManId, EmpProId, PerBasFirstName, PerBasLastName FROM EmployeeInfo INNER JOIN EmploymentManagementInfo ON Emp_EmpManId = EmpManId " +
                $"INNER JOIN EmploymentProfessionInfo ON Emp_EmpProId = EmpProId INNER JOIN EmployeeAccount ON EmpAcc_EmpId = EmpId " +
                $"INNER JOIN PersonContactInfo ON PerCon_PerBasId = Emp_PerBasId INNER JOIN PersonBasicInfo ON PerBasId = Emp_PerBasId " +
                $"WHERE (EmpProId = 2 OR EmpManId = 5 OR EmpManId = 6) " +
                $"AND (PerConEmail = '{email}' AND EmpAccPassword = '{password}');";
            SqlCommand command = new SqlCommand(logInHR, conn);

            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                LoginEmployeeModel = new EmployeeModel(
                    Convert.ToInt32(reader["EmpId"]),
                    reader["PerBasFirstName"].ToString(),
                    reader["PerBasLastName"].ToString(),
                    Convert.ToInt32(reader["EmpManId"]),
                    Convert.ToInt32(reader["EmpProId"]));
            }
            conn.Close();

            if (IsDataCorrect) 
                return LoginEmployeeModel;
            else return LoginEmployeeModel;
        }
    }
}
