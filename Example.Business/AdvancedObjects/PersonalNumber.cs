using System;

namespace Example.Business.AdvancedObjects
{
    public class PersonalNumber
    {
        public PersonalNumber(string PersonalNumber)
        {
            //Logic to populate values
        }

        public bool IsValid { get; set; }
        public string OriginalString { get; set; }
        public string OfficialFormat { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
    }
}