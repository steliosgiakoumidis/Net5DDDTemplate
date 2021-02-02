using System;
using Example.Business.AdvancedObjects;

namespace Example.Business.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public PersonalNumber PersonalNumber { get; set; }
    }
}