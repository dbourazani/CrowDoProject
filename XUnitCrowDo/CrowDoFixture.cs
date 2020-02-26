using CrowDo.Core.Data;
using CrowDo.Models;
using CrowDo.Services;
using System;


namespace XUnitCrowDo
{
    public class CrowDoFixture : IDisposable
    {
        public CrowDoDbContext Context { get; private set; }
        public IUserService Users{ get; private set; }
        public IProjectService Projects { get; private set; }

        public IFundingPackageService FundingPackages { get; private set; }

        public CrowDoFixture()
        {
            Context = new CrowDoDbContext();
            Users = new UserService(Context);
            //Projects = new ProjectService(Context);
            //FundingPackages = new FundingPackage(Context);

        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
