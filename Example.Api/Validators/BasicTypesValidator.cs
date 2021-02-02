using System;
using Example.Business.AdvancedObjects;

namespace Example.Api.Validators
{
    public class BasicTypesValidator
    {
        public bool StringValidator(string stringQuery)
        {
            return String.IsNullOrEmpty(stringQuery) 
                   || String.IsNullOrWhiteSpace(stringQuery);
        }
    }
}