using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.ModelsPerson
{
    public class PersonAdressModel
    {
        public int PerId { get; private set; }
        public string PerAdrCountry { get; private set; }
        public string PerAdrCity { get; private set; }
        public string PerAdrStreet { get; private set; }
        public string PerAdrZipCode { get; private set; }

        public PersonAdressModel(int perId, string perCountry, string perCity, string perStreet, string perZipCode)
            :this(perCountry, perCity, perStreet, perZipCode)
        {
            PerId = perId;
        }
        public PersonAdressModel(string perCountry, string perCity, string perStreet, string perZipCode)
        {
            PerAdrCountry = perCountry;
            PerAdrCity = perCity;
            PerAdrStreet = perStreet;
            PerAdrZipCode = perZipCode;
        }
    }
}
