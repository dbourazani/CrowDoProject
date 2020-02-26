using CrowDo.Models;
using CrowDo.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CrowDo.Services
{
    public interface IFundingPackageService
    {
        FundingPackage CreateFundingPackage(CreateFundingPackageOptions options);
        FundingPackage Fund(int userId, int fundId);
        List<FundingPackage> SearchFundingPackages(int  projectId);
        public FundingPackage GetFundingPackageById(int id);

        //bool UpdateFundingPackage(UpdateFundingPackageOptions options, int id);
    }
}