using Example.Business.AdvancedObjects;
using Example.Business.Models;

namespace Example.Infrastructure.Transformers
{
    public class UserTransformersInfrastructure
    {
        public User InfrastructureToModel(Models.User user)
        {
            return new User()
            {
                Name = user.Name,
                Surname = user.Surname,
                PersonalNumber = new PersonalNumber(user.PersonalNumber)
            };
        }

        public Models.User ModelToInfrastructure(User user)
        {
            return new Models.User()
            {
                Name = user.Name,
                Surname = user.Surname,
                PersonalNumber = user.PersonalNumber.OfficialFormat
            };
        }
    }
    
}