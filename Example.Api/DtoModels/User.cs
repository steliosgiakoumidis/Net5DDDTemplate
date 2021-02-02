using System.ComponentModel.DataAnnotations;
using Example.Api.Validators;
using Microsoft.AspNetCore.Authentication;

namespace Example.Api.DtoModels
{
    public class User
    {
        [Required(ErrorMessage = "Name is required"), StringLength(20, ErrorMessage = "Name length can't be more than 20")]
        public string Name { get; init; }
        [Required(ErrorMessage = "Surname is required"), StringLength(20, ErrorMessage = "Name length can't be more than 20")]
        public string Surname { get; init; }
        [UserCustomValidator(ErrorMessage = "Personal number is not valid")]
        public string PersonalNumber { get; init; }
    }
}