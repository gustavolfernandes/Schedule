
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Schedule.domain.Entities;
using Schedule.Application.Services;
using Schedule.domain.Helpers;
using Schedule.domain.Interfaces.Repositories;

namespace Schedule.Data.Repositories
{
    //https://localhost:5001/users
    public class UserRepository : IUserRepository
    {
        protected readonly DataContext context;
        public UserRepository(DataContext context)
        {
            this.context = context;

        }
        //Lista de user
        public async Task<List<User>> Get()
        {
            var users = await context
                .Users
                .AsNoTracking()
                .ToListAsync();
            return users;
        }

        //Procura user por Id
        public async Task<User> GetById(int id)
        {
            var user = await context
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }
        //Registra user
        public async Task<User>Post(User model)
        {
            try
            {
                model.Password = PasswordHasher.GenerateHasher(model.Password);
                context.Users.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Login user
        public async Task<dynamic>Authenticate(User model)
        {
            try
            {
                model.Password = PasswordHasher.GenerateHasher(model.Password);
                var user = await context.Users
                    .AsNoTracking()
                    .Where(x => x.Username == model.Username && x.Password == model.Password)
                    .FirstOrDefaultAsync();

                user.Token = TokenService.GenerateToken(model);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}