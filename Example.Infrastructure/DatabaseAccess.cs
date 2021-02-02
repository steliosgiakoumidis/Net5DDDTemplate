using System;
using System.Data;
using System.Threading.Tasks;
using Example.Business.Interfaces;
using Example.Business.Models;
using Example.Infrastructure.Transformers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Example.Infrastructure
{
    public class DatabaseAccess : IDatabaseAccess
    {
        private readonly ApplicationContext _context;
        private ILogger _logger;
        private readonly UserTransformersInfrastructure _transformers;

        public DatabaseAccess(ApplicationContext context, ILogger<DatabaseAccess> logger, UserTransformersInfrastructure transformers)
        {
            _context = context;
            _logger = logger;
            _transformers = transformers;
        }

        public async Task<User> GetUser(string name)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == name);
            return user is null ? null : _transformers.InfrastructureToModel(user);
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(_transformers.ModelToInfrastructure(user));
        }

        public async Task UpdateUser(User user)
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Name == user.Name);
            if (currentUser is null)
                throw new Exception($"User with name: {user.Name} did not exist");

            _context.Users.Update(_transformers.ModelToInfrastructure(user));
        }

        public async Task DeleteUser(string name)
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Name == name);
            if (currentUser is null)
                throw new Exception($"User with name: {name} did not exist");

            _context.Users.Remove(currentUser);        
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}