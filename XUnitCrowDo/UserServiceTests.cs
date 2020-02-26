using CrowDo.Core.Data;
using CrowDo.Models;
using CrowDo.Options;
using CrowDo.Services;
using System;
using System.Linq;
using Xunit;



namespace XUnitCrowDo
{
    public class UserServiceTests : IClassFixture<CrowDoFixture>
    {
        private CrowDoDbContext context_;
        private IUserService user_;

        public UserServiceTests(CrowDoFixture fixture)
        {
            context_ = fixture.Context;
            user_ = fixture.Users;
        }
        [Fact]
        public User CreateUserSuccess()
        {
            var options = new CreateUserOptions()
            {
                Email = "dimitra@hotmail.gr",
                FirstName = "dimitra",
                LastName = "bourazani",
                YearOfBirth = 1993,
                Address = "Athens"
            };
            var user = user_.CreateUser(options);
            Assert.NotNull(user);
            Assert.Equal(options.Email, user.Email);
            Assert.Equal(options.FirstName, user.FirstName);
            Assert.Equal(options.LastName, user.LastName);
            Assert.Equal(options.YearOfBirth, user.YearOfBirth);
            Assert.Equal(options.Address, user.Address);

            var options1 = new SearchUserOptions()
            {
                Email = options.Email,
                FirstName = options.FirstName,
                LastName = options.LastName,
                YearOfBirth = options.YearOfBirth,
                Address = options.Address
            };

            var query = user_.SearchUser(options1)
                .Where(u => u.Id == user.Id)
                .SingleOrDefault();
            Assert.NotNull(query);
            Assert.Equal(user.Id, query.Id);
            return user;
        }

        [Fact]
        public void CreateCustomerFailEmail()
        {
            var options = new CreateUserOptions()
            {
                Email = "dimitrahotmail.gr",
                FirstName = "dimitra",
                LastName = "bourazani"
            };

            var user = user_.CreateUser(options);

            Assert.Null(user);
            
            options = new CreateUserOptions()
            {
                Email = " ",
                FirstName = "dimitra",
                LastName = "bourazani"
            };

            user = user_.CreateUser(options);
            Assert.Null(user);
            
            options = new CreateUserOptions()
            {
                Email = null,
                FirstName = "dimitra",
                LastName = "bourazani"
            };

            user = user_.CreateUser(options);
            Assert.Null(user);
        }
    }
}
