using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.ClassesModels
{
    public class PersonContactModel
    {
        public int PerId { get; private set; }
        public string PerEmail { get; private set; }
        public string PerPhone { get; private set; }

        public PersonContactModel(string perEmail, string perPhone)
        {
            PerEmail = perEmail;
            PerPhone = perPhone;
        }
        public PersonContactModel(int perId, string perEmail, string perPhone) :this(perEmail, perPhone)
        {
            PerId = perId;
        }
    }
}
