using System;
using System.Data;
using System.Data.Common;
using System.Text.Json;
using System.Threading.Tasks;
using Example.Business.Exceptions;
using Example.Business.Interfaces;
using Example.Business.Models;
using Microsoft.Extensions.Logging;

namespace Example.Business.Handlers
{
    public class UserHandler : IUserHandlers
    {
        private readonly IDatabaseAccess _databaseAccess;
        private readonly ILogger _logger;

        public UserHandler(IDatabaseAccess databaseAccess, ILogger<UserHandler> logger)
        {
            _logger = logger;
            _databaseAccess = databaseAccess;
        }


        public async Task<User> GetUser(string name)
        {
            try
            {
                return await _databaseAccess.GetUser(name);
                
            }
            catch (Exception e)
            {
                throw new DatabaseLayerException($"Error on GetUser({name})", e);
            }
        }

        public async Task DeleteUser(string name)
        {
            try
            {
                await _databaseAccess.DeleteUser(name);
                await _databaseAccess.Save();
            }
            catch (Exception e)
            {
                throw new DatabaseLayerException($"Error on DeleteUser({name})", e);
            }
        }

        public async Task AddUser(User user)
        {
            try
            {
                await _databaseAccess.AddUser(user);
                await _databaseAccess.Save();
            }
            catch (Exception e)
            {
                throw new DatabaseLayerException($"Error on AddUser({JsonSerializer.Serialize(user)})", e);
            }
        }

        public async Task UpdateUser(User user)
        {
            try
            {
                await _databaseAccess.UpdateUser(user);
                await _databaseAccess.Save();
            }
            catch (Exception e)
            {
                throw new DatabaseLayerException($"Error on UpdateUser({JsonSerializer.Serialize(user)})", e);
            }
        }
    }
}