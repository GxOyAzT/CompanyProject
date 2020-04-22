using ClassLibrary.Schedule;
using ClassLibrary.ClassesModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using ClassLibrary.ModelsSchedule;

namespace ClassLibrary.DatabaseConnections
{
    public static class ScheduleDbConn
    {
        static SqlConnection conn = new SqlConnection("Server = localhost; Integrated security = SSPI; database=Company");

        public static void InsertIntoSchedule (ScheduleModel newScheduleModel)
        {
            string sqlInsertNew = "INSERT INTO Schedule (Sch_EmpId, SchDate, SchStart, SchEnd) " +
                $"VALUES ({newScheduleModel.SchEmployeeModel.EmpId}, '{newScheduleModel.SchDate.ToString("yyyy-MM-dd")}' , '{newScheduleModel.SchStart.Hours}:{newScheduleModel.SchEnd.Minutes}:{newScheduleModel.SchEnd.Seconds}', " +
                $"'{newScheduleModel.SchEnd.Hours}:{newScheduleModel.SchEnd.Minutes}:{newScheduleModel.SchEnd.Seconds}');";
            SqlCommand command = new SqlCommand(sqlInsertNew, conn);

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static List<ScheduleModel> LoadScheduleModelsOnDate (DateTime dateTime)
        {
            List<ScheduleModel> scheduleModels = new List<ScheduleModel>();
            string sqlLoadScheduleOnDate = $"SELECT emp.EmpId, sch.SchId, sch.Sch_EmpId, sch.SchDate, SchEnd, SchStart, pbi.PerBasFirstName, pbi.PerBasLastName, " +
                $"emi.EmpManName, epi.EmpProName FROM  Schedule sch LEFT JOIN EmployeeInfo emp ON emp.EmpId = sch.Sch_EmpId " +
                $"LEFT JOIN PersonBasicInfo pbi ON emp.Emp_PerBasId = pbi.PerBasId LEFT JOIN EmploymentManagementInfo emi ON emi.EmpManId = emp.Emp_EmpManId " +
                $"LEFT JOIN EmploymentProfessionInfo epi ON emp.Emp_EmpProId = epi.EmpProId WHERE sch.SchDate = '{dateTime.ToString("yyyy-MM-dd")}'";
            SqlCommand command = new SqlCommand(sqlLoadScheduleOnDate, conn);

            conn.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                scheduleModels.Add(new ScheduleModel(
                    Convert.ToInt32(reader["SchId"]),
                    Convert.ToDateTime(reader["SchDate"]),
                    (TimeSpan)reader["SchStart"],
                    (TimeSpan)reader["SchEnd"],
                    new EmployeeModel(
                        Convert.ToInt32(reader["EmpId"]), 
                        new PersonModel(
                            reader["PerBasFirstName"].ToString(),
                            reader["PerBasLastName"].ToString()),
                    new ManagementModel(reader["EmpManName"].ToString()),
                    new ProfessionModel(reader["EmpProName"].ToString()))
                    ));
            }
            conn.Close();

            return scheduleModels;
        }
        public static void DeleteShiftFromSchedule (int schId)
        {
            string sqlDeleteShift = $"DELETE FROM Schedule WHERE SchId = {schId};";
            SqlCommand command = new SqlCommand(sqlDeleteShift, conn);

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static List<EmployeeModel> SelectDistinctEmployeeOnMonth(DateTime dateTime)
        {
            List<EmployeeModel> employeeModels = new List<EmployeeModel>();
            string sqlDistinctEmp = $"SELECT ei.EmpId, pbi.PerBasFirstName, pbi.PerBasLastName, emi.EmpManName, emi.EmpManSalaryIncrease, epi.EmpProName, epi.EmpProSalary FROM " +
                $"(SELECT DISTINCT s.Sch_EmpId FROM Schedule s WHERE s.SchDate >= '{dateTime.ToString("yyyy-MM-dd")}' and s.SchDate < '{dateTime.AddMonths(1).ToString("yyyy-MM-dd")}') x " +
                $"LEFT JOIN EmployeeInfo ei ON ei.EmpId = x.Sch_EmpId LEFT JOIN PersonBasicInfo pbi ON pbi.PerBasId = ei.Emp_PerBasId " +
                $"LEFT JOIN EmploymentManagementInfo emi ON emi.EmpManId = ei.Emp_EmpManId LEFT JOIN EmploymentProfessionInfo epi ON epi.EmpProId = ei.Emp_EmpProId";

            SqlCommand command = new SqlCommand(sqlDistinctEmp, conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                employeeModels.Add(new EmployeeModel(
                    Convert.ToInt32(reader["EmpId"]),
                    new PersonModel(
                        reader["PerBasFirstName"].ToString(),
                        reader["PerBasLastName"].ToString()),
                    new ManagementModel(
                        reader["EmpManName"].ToString(),
                        Convert.ToInt32(reader["EmpManSalaryIncrease"])),
                    new ProfessionModel(
                        reader["EmpProName"].ToString(),
                        Convert.ToInt32(reader["EmpProSalary"]))
                    ));
            }
            conn.Close();

            return employeeModels;
        }
        public static List<ScheduleModel> SelectSchModelOnEmpAndDate(int empId, DateTime dateTime)
        {
            List<ScheduleModel> scheduleModels = new List<ScheduleModel>();
            string sqlSelect = $"SELECT * FROM Schedule WHERE Sch_EmpId = {empId} AND " +
                $"SchDate >= '{dateTime.ToString("yyyy-MM-dd")}' AND SchDate < '{dateTime.AddMonths(1).ToString("yyyy-MM-dd")}';";
            SqlCommand command = new SqlCommand(sqlSelect, conn);

            conn.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                scheduleModels.Add( new ScheduleModel(
                    empId,
                    Convert.ToDateTime(reader["SchDate"]),
                    (TimeSpan)reader["SchStart"],
                    (TimeSpan)reader["SchEnd"],
                    Convert.ToInt32(reader["SchId"])
                    ));
            }

            conn.Close();
            return scheduleModels;
        }
        public static List<ScheduleModel> SelectSchModelOnDateToDate(DateTime dateTime)
        {
            List<ScheduleModel> scheduleModels = new List<ScheduleModel>();
            string sqlSelect = $"SELECT * FROM Schedule WHERE " +
                $"SchDate >= '{dateTime.ToString("yyyy-MM-dd")}' AND SchDate < '{dateTime.AddMonths(1).ToString("yyyy-MM-dd")}';";
            SqlCommand command = new SqlCommand(sqlSelect, conn);

            conn.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                scheduleModels.Add( new ScheduleModel(
                    Convert.ToInt32(reader["Sch_EmpId"]),
                    Convert.ToDateTime(reader["SchDate"]),
                    (TimeSpan)reader["SchStart"],
                    (TimeSpan)reader["SchEnd"],
                    Convert.ToInt32(reader["SchId"])
                    ));
            }

            conn.Close();
            return scheduleModels;
        }
        public static List<ScheduleModel> SelectAllSchModels()
        {
            List<ScheduleModel> scheduleModels = new List<ScheduleModel>();
            string sqlSelect = $"SELECT * FROM Schedule;";
            SqlCommand command = new SqlCommand(sqlSelect, conn);

            conn.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                scheduleModels.Add(new ScheduleModel(
                    Convert.ToInt32(reader["Sch_EmpId"]),
                    Convert.ToDateTime(reader["SchDate"]),
                    (TimeSpan)reader["SchStart"],
                    (TimeSpan)reader["SchEnd"],
                    Convert.ToInt32(reader["SchId"])
                    ));
            }

            conn.Close();
            return scheduleModels;
        }

        public static List<YearSummarySalaryModel> SelectYearSummarySalaryModelByYear (int year)
        {
            List<YearSummarySalaryModel> yearSummarySalaryModels = new List<YearSummarySalaryModel>();

            string sql = $"EXECUTE sp_Schedule_GetWholeYearSalary {year}";
            SqlCommand command = new SqlCommand(sql, conn);

            conn.Open();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yearSummarySalaryModels.Add(new YearSummarySalaryModel(
                        reader["FullName"].ToString(),
                        new double[12] { Convert.ToDouble(reader["1"]), Convert.ToDouble(reader["2"]), Convert.ToDouble(reader["3"]), Convert.ToDouble(reader["4"]), Convert.ToDouble(reader["5"]), Convert.ToDouble(reader["6"]), Convert.ToDouble(reader["7"]), Convert.ToDouble(reader["8"]), Convert.ToDouble(reader["9"]), Convert.ToDouble(reader["10"]), Convert.ToDouble(reader["11"]), Convert.ToDouble(reader["12"]) }
                        ));
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            finally
            {
                conn.Close();
            }

            return yearSummarySalaryModels;
        }
    }
}
