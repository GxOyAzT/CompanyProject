using ClassLibrary.ClassesModels;
using ClassLibrary.ModelsPerson;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class PersonModel
    {
        public int PerId { get; private set; }
        public string PerFirstName { get; private set; }
        public string PerLastName { get; private set; }
        public char PerGender { get; private set; }
        public DateTime PerDob { get; private set; }
        public PersonContactModel PerContactModel { get; private set; }
        public PersonAdressModel PerAdressModel { get; set; }
        public string PerFullName { get => $"{PerLastName} {PerFirstName}"; }
        public string PerDobString { get => $"{PerDob.ToString("yyyy-MM-dd")}"; }

        public PersonModel(int id, string firstName, string lastName, char gender, DateTime dob)
            :this(firstName, lastName, gender, dob)
        {
            PerId = id;
        }
        public PersonModel(string firstName, string lastName, char gender, DateTime dob)
        {
            PerFirstName = firstName;
            PerLastName = lastName;
            PerGender = gender;
            PerDob = dob;
        }
        public PersonModel(int id, string firstName, string lastName, char gender, DateTime dob, string email, 
            string phone, string country, string city, string street, string zipCode):
            this(firstName, lastName, gender, dob, email, phone, country, city, street, zipCode)
        {
            PerId = id;
        }
        public PersonModel(string firstName, string lastName, char gender, DateTime dob, string email, 
            string phone, string country, string city, string street, string zipCode):
            this(firstName, lastName, gender, dob)
        {
            PerContactModel = new PersonContactModel(email, phone);
            PerAdressModel = new PersonAdressModel(country, city, street, zipCode);
        }
        public PersonModel(string firstName, string lastName)
        {
            PerFirstName = firstName;
            PerLastName = lastName;
        }
        public PersonModel(int id, string firstName, string lastName)
        {
            PerId = id;
            PerFirstName = firstName;
            PerLastName = lastName;
        }
        public PersonModel() => PerId = -1;

        public void CompleteConAdrModel(PersonContactModel con, PersonAdressModel adr)
        {
            PerContactModel = con;
            PerAdressModel = adr;
        }
        public void CompleteConModel(PersonContactModel con) => PerContactModel = con;
        public void CompleteAdrModel(PersonAdressModel adr) => PerAdressModel = adr;
    }
}