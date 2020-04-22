using ClassLibrary.ClassesModels;
using ClassLibrary.ModelsPerson;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ClassLibrary.DatabaseConnections
{
    public static class PersonDbConn
    {
        static SqlConnection conn = new SqlConnection("Server = localhost; Integrated security = SSPI; database=Company");

        public static List<PersonModel> GetFullPersonInfo()
        {
            List<PersonModel> personInfos = new List<PersonModel>();
            string getFullPersonInfo = "EXECUTE sp_PersonInfo_GetAll";
            SqlCommand command = new SqlCommand(getFullPersonInfo, conn);
            conn.Open();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    personInfos.Add(new PersonModel(Convert.ToInt32(reader["PerBasId"]), reader["PerBasFirstName"].ToString(), reader["PerBasLastName"].ToString(),
                        Convert.ToChar(reader["PerBasGender"]), Convert.ToDateTime(reader["PerBasDob"]), reader["PerConEmail"].ToString(),
                        reader["PerConPhoneNumber"].ToString(), reader["PerAdrCountry"].ToString(), reader["PerAdrCity"].ToString(),
                        reader["PerAdrStreet"].ToString(), reader["PerAdrZipCode"].ToString()));
                }
            }
            catch(SqlException ex)
            {
                Console.WriteLine($"DATABASE EXCEPTION\n{ex.Message}");
            }
            finally
            {
                conn.Close();
            }
            return personInfos;
        }
        public static List<PersonModel> GetFullPersonInfoAreNotEmployeed()
        {
            List<PersonModel> personInfos = new List<PersonModel>();
            string getFullPersonInfo = "EXECUTE sp_PersonInfo_GetAllUnemployeed";
            SqlCommand command = new SqlCommand(getFullPersonInfo, conn);
            conn.Open();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    personInfos.Add(new PersonModel(Convert.ToInt32(reader["PerBasId"]), reader["PerBasFirstName"].ToString(), reader["PerBasLastName"].ToString(),
                        Convert.ToChar(reader["PerBasGender"]), Convert.ToDateTime(reader["PerBasDob"]), reader["PerConEmail"].ToString(),
                        reader["PerConPhoneNumber"].ToString(), reader["PerAdrCountry"].ToString(), reader["PerAdrCity"].ToString(),
                        reader["PerAdrStreet"].ToString(), reader["PerAdrZipCode"].ToString()));
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"DATABASE EXCEPTION\n{ex.Message}");
            }
            finally
            {
                conn.Close();
            }
            return personInfos;
        }
        public static PersonModel GetPersonFnLnById(int id)
        {
            PersonModel personModel = new PersonModel();
            string getPersonFnLnById = $"EXECUTE sp_PersonInfo_GetFstLastNameById {id}";
            SqlCommand command = new SqlCommand(getPersonFnLnById, conn);

            conn.Open();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    personModel = new PersonModel(Convert.ToInt32(id), reader["PerBasFirstName"].ToString(), reader["PerBasLastName"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"DATABASE EXCEPTION\n{ex.Message}");
            }
            finally
            {
                conn.Close();
            }

            return personModel;
        }
        public static void InsertFullPersonInfo(PersonModel per)
        {
            string insertFullPersonInfo = $"EXECUTE sp_PersonInfo_InsertFullPerInf {per.PerFirstName}, {per.PerLastName}, {per.PerGender}, {per.PerDob}, {per.PerAdressModel.PerAdrCountry}, {per.PerAdressModel.PerAdrCity}, {per.PerAdressModel.PerAdrStreet}, {per.PerAdressModel.PerAdrZipCode}, {per.PerContactModel.PerPhone}, {per.PerContactModel.PerEmail}";
            SqlCommand command = new SqlCommand(insertFullPersonInfo, conn);
            conn.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"DATABASE EXCEPTION\n{ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }
        public static void UpdateFullPersonInfo(PersonModel per)
        {
            string updateFullPersonInfo = $"EXECUTE sp_PersonInfo_UpdateFullPersonInfo {per.PerId}, {per.PerFirstName}, {per.PerLastName}, {per.PerGender}, {per.PerDob}, {per.PerAdressModel.PerAdrCountry}, {per.PerAdressModel.PerAdrCity}, {per.PerAdressModel.PerAdrStreet}, {per.PerAdressModel.PerAdrZipCode}, {per.PerContactModel.PerPhone}, {per.PerContactModel.PerEmail}";
            SqlCommand command = new SqlCommand(updateFullPersonInfo, conn);
            conn.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"DATABASE EXCEPTION\n{ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }
        public static List<PersonContactModel> GetFullPerConModel()
        {
            List<PersonContactModel> personContactModels = new List<PersonContactModel>();
            string sqlSelect = "EXECUTE sp_PersonInfo_GetAllCon";
            SqlCommand command = new SqlCommand(sqlSelect, conn);

            conn.Open();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    personContactModels.Add(new PersonContactModel(
                        Convert.ToInt32(reader["PerCon_PerBasId"]),
                        reader["PerConPhonenumber"].ToString(),
                        reader["PerConEmail"].ToString()));
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"DATABASE EXCEPTION\n{ex.Message}");
            }
            finally
            {
                conn.Close();
            }
            return personContactModels;
        }
        public static List<PersonAdressModel> GetFullPerAdrModel()
        {
            List<PersonAdressModel> personAdressModels = new List<PersonAdressModel>();
            string sqlSelect = "EXECUTE sp_PersonInfo_GetAllAdr";
            SqlCommand command = new SqlCommand(sqlSelect, conn);

            conn.Open();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    personAdressModels.Add(new PersonAdressModel(
                        Convert.ToInt32(reader["PerAdr_PerBasId"]),
                        reader["PerAdrCountry"].ToString(),
                        reader["PerAdrCity"].ToString(),
                        reader["PerAdrStreet"].ToString(),
                        reader["PerAdrZipCode"].ToString()
                        ));
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"DATABASE EXCEPTION\n{ex.Message}");
            }
            finally
            {
                conn.Close();
            }
            return personAdressModels;
        }
        public static List<PersonModel> GetPerBasModel()
        {
            List<PersonModel> personBasicModels = new List<PersonModel>();
            string sqlSelect = "EXECUTE sp_PersonInfo_GetAllBas";
            SqlCommand command = new SqlCommand(sqlSelect, conn);

            conn.Open();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    personBasicModels.Add(new PersonModel(
                        Convert.ToInt32(reader["PerBasId"]),
                        reader["PerBasFirstName"].ToString(),
                        reader["PerBasLastName"].ToString(),
                        Convert.ToChar(reader["PerBasGender"]),
                        Convert.ToDateTime(reader["PerBasDob"])
                        ));
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"DATABASE EXCEPTION\n{ex.Message}");
            }
            finally
            {
                conn.Close();
            }
            return personBasicModels;
        }
    }
}
