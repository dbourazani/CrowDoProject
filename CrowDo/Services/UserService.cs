using CrowDo.Models;
using CrowDo.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowDo.Core.Data;
namespace CrowDo.Services
{
    public class UserService : IUserService
    {
        private readonly CrowDoDbContext context_;
        public UserService(CrowDoDbContext context)
        {
            context_ = context;
        }
        public User CreateUser(CreateUserOptions userOptions)
        {

            if (userOptions == null)
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(userOptions.FirstName) ||
                string.IsNullOrWhiteSpace(userOptions.LastName) ||
                string.IsNullOrWhiteSpace(userOptions.Address) ||
                string.IsNullOrWhiteSpace(userOptions.Email) ||
                !userOptions.Email.Contains("@") ||
                !userOptions.YearOfBirth.HasValue)
            {
                return null;
            }

            var user = new User
            {
                FirstName = userOptions.FirstName,
                LastName = userOptions.LastName,
                Address = userOptions.Address,
                Email = userOptions.Email,
                YearOfBirth = userOptions.YearOfBirth,
                Status =StatusUser.Active ,
            };
            context_.Set<User>().Add(user);
            //if email is not unique
            try
            {
                context_.SaveChanges();
                return user;
            }
            catch(Exception)
            { 
                return null; 
            }
            
        }
        public List<User> GetUsers()
        {
            return context_.Set<User>()
                    .ToList();
        }
        public List<User> SearchUser(SearchUserOptions userOptions)
        {
            if (userOptions == null)
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(userOptions.Email) &&
               (userOptions.Id == null || userOptions.Id == 0) &&
               string.IsNullOrWhiteSpace(userOptions.FirstName) &&
               string.IsNullOrWhiteSpace(userOptions.LastName))
            {
                return null;
            }

            var query = context_
               .Set<User>()
               .AsQueryable();

            if (userOptions.Id != null && userOptions.Id != 0)
            {
                query = query.Where(
                    c => c.Id == userOptions.Id);
            }

            if (!string.IsNullOrWhiteSpace(userOptions.FirstName))
            {
                query = query
                      .Where(c => c.FirstName.Contains(userOptions.FirstName));
            }

            if (userOptions.LastName != null)
            {
                query = query
                        .Where(c => c.LastName == userOptions.LastName);
            }

            if (userOptions.Email != null ||
                userOptions.Email.Contains("@"))
            {
                query = query
                        .Where(c => c.Email == userOptions.Email);
            }

            return query.ToList();
        }

        public User GetUserById(int id)
        {
            var user = context_
                .Set<User>()
                .SingleOrDefault(s => s.Id == id);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public User UpdateUser(int id,
            UpdateUserOptions options)
        {
            if (options == null)
            {
                return null;
            }
            try
            {
                var user = GetUserById(id);
                if (user == null)
                {
                    return null;
                }

                if (!string.IsNullOrWhiteSpace(options.FirstName))
                {
                    user.FirstName = options.FirstName;
                }

                if (!string.IsNullOrWhiteSpace(options.LastName))
                {
                    user.LastName = options.LastName;
                }

                if (!string.IsNullOrWhiteSpace(options.Address))
                {
                    user.Address = options.Address;
                }

                if (!string.IsNullOrWhiteSpace(options.Email) &&
                    options.Email.Contains("@"))
                {
                    user.Email = options.Email;
                }

                context_.SaveChanges();
                return user;
            }
            catch (Exception)
            { 
                return null; 
            }
        }
    }
}