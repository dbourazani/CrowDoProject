using CrowDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CrowDo.Options
{
    public class CreateFundingPackageOptions
    {
        public int ProjectId { get; set; } //creator
        public decimal Deposit { get; set; }
        public string DescriptionGift { get; set; }
        public decimal FixedPackageAmount { get; set; }
    }
}