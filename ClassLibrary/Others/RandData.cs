using ClassLibrary.DatabaseConnections;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

namespace ClassLibrary.Others
{
    public static class RandData
    {
        static int FindAge(string text)
        {
            string subText = text.Substring(text.IndexOf("<dt>Age</dt>") + 21, 100);
            int inxexOf_yearsOld = subText.LastIndexOf(" years");
            subText = subText.Substring(0, subText.LastIndexOf(" years"));
            return Convert.ToInt32(subText);
        }
        static string FindFullName(string text)
        {
            string subText = text.Substring(text.IndexOf("<div class=\"address\">") + 21, 100);
            subText = subText.Substring(subText.LastIndexOf("<h3>"), 30);
            subText = subText.Substring(4, subText.LastIndexOf("h3>") - 6);
            return subText;
        }
        static string FindPhoneNumber(string text)
        {
            string subText = text.Substring(text.IndexOf("<dt>Phone</dt>") + 21, 30);
            subText = subText.Substring(subText.LastIndexOf("<dd>"), 22);
            subText = subText.Substring(4, subText.LastIndexOf("</dd>") - 4);
            string phone = "";
            phone += subText[0];
            phone += subText[1];
            phone += subText[3];
            phone += subText[4];
            phone += subText[5];
            phone += subText[7];
            phone += subText[8];
            phone += subText[10];
            phone += subText[11];
            return phone;
        }
        static char FindGender(string name)
        {
            if (name[name.Length - 1] == 'a') return 'K';
            else return 'M';
        }
        static DateTime FindDob(int age)
        {
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddYears(-age);
            dateTime = dateTime.AddDays((new Random().Next(0, 365)));
            return dateTime;
        }
        //
        static string FindCity(string page)
        {
            string subText = page.Substring(page.IndexOf("<div class=\"adr\">") + 21, 200);
            subText = subText.Substring(subText.LastIndexOf("<br ") + 13, 80);
            subText = subText.Substring(0, subText.IndexOf("<") - 40);
            return subText;
        }
        static string FindZipCode(string page)
        {
            string subText = page.Substring(page.IndexOf("<div class=\"adr\">") + 21, 200);
            subText = subText.Substring(subText.IndexOf(">") + 1, 6);
            return subText;
        }
        static string FindStreet(string page)
        {
            string subText = page.Substring(page.IndexOf("<div class=\"adr\">") + 21, 100);
            subText = subText.Substring(subText.LastIndexOf("ul.") + 4, 30);
            subText = subText.Substring(0, subText.LastIndexOf("<"));
            return subText;
        }
        public static void SearchRandomData()
        {
            string text = "heh heh";
            Console.WriteLine(text.IndexOf(" "));
            while (true)
            {
                WebClient web = new WebClient();
                System.IO.Stream stream = web.OpenRead("https://www.fakenamegenerator.com/gen-random-pl-pl.php");
                string page = "";
                using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
                {
                    page = reader.ReadToEnd();
                    //Console.WriteLine(page);
                    //Console.ReadKey();
                }
                Thread.Sleep(1000);
                try
                {
                    Console.WriteLine();
                    string fullName = FindFullName(page);
                    string firstName = fullName.Substring(0, fullName.LastIndexOf(" "));
                    string lastName = fullName.Substring(fullName.LastIndexOf(" ") + 1, fullName.Length - 1 - fullName.LastIndexOf(" "));
                    if (firstName == null | lastName == null) break;
                    string phoneNumber = FindPhoneNumber(page);
                    char gender = FindGender(firstName);
                    DateTime dob = FindDob(FindAge(page));
                    string email = firstName.ToLower() + "." + lastName.ToLower() + "@gmail.com";
                    string country = "Polska";
                    string city = FindCity(page);
                    string zipCode = FindZipCode(page);
                    string street = FindStreet(page);
                    PersonModel newPerson = new PersonModel(firstName, lastName, gender, dob, email, phoneNumber, country, city, street, zipCode);
                    Console.WriteLine($"First name: {firstName}");
                    Console.WriteLine($"Last name: {lastName}");
                    Console.WriteLine($"Phone number: {phoneNumber}");
                    Console.WriteLine($"Gender: {gender}");
                    Console.WriteLine($"Dob: {dob}");
                    Console.WriteLine($"Email: {email}");
                    Console.WriteLine($"Nationality: {country}");
                    Console.WriteLine($"City: {city}");
                    Console.WriteLine($"Street: {street}");
                    Console.WriteLine($"ZipCode: {zipCode}");
                    PersonDbConn.InsertFullPersonInfo(newPerson);
                }
                catch
                {
                    Console.WriteLine("ERROR");
                }
                finally
                {

                }
            }
        }
    }
}
