using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ClassLibrary.Others
{
    public static class OtherFunctions
    {
        static SqlConnection conn = new SqlConnection("Server = localhost; Integrated security = SSPI; database=Company");
        public static string GenerateRandomPassword(int howManyElements)
        {
            string pass = "";
            for(int i = 0; i < howManyElements; i++)
            {
                int randNumb = (new Random()).Next(0, 61);
                if(randNumb < 10)
                {
                    pass += Convert.ToChar(48 + randNumb);
                }
                else if (randNumb >= 10 & randNumb < 36)
                {
                    pass += Convert.ToChar(65 + randNumb - 10);
                }
                else
                {
                    pass += Convert.ToChar(97 + randNumb - 36);
                }
            }
            return pass;
        }

        public static void SqlExercise()
        {
            string sql = $"SELECT NULL AS XD";
            SqlCommand command = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            string ans = reader["XD"] == null ? "Null" : "Not Null";
            conn.Close();

            int [] tab = { 1, 2, 3 };
            int x = tab[0];
        }
        
    }
}
