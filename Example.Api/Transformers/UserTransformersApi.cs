using Example.Api.DtoModels;
using Example.Business.AdvancedObjects;

namespace Example.Api.Transformers
{
    public class UserTransformersApi
    {
        public User ModelToDto(Business.Models.User user)
        {
            return new User()
            {
                Name = user.Name,
                Surname = user.Surname,
                PersonalNumber = user.PersonalNumber.OfficialFormat
            };
        }
        
        public Business.Models.User DtoToModel(User user)
        {
            return new Business.Models.User()
            {
                Name = user.Name,
                Surname = user.Surname,
                PersonalNumber = new PersonalNumber(user.PersonalNumber)
            };
        }
    }
}