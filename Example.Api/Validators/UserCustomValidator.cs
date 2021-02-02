using System;
using System.ComponentModel.DataAnnotations;
using Example.Api.DtoModels;
using Example.Business.AdvancedObjects;

namespace Example.Api.Validators
{
    public class UserCustomValidator : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var personalNumber = new PersonalNumber(value.ToString());

            personalNumber.IsValid = true;
            return personalNumber.IsValid;
        }

    }
}