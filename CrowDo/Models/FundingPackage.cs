using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CrowDo.Models
{
    public class FundingPackage
    {
        public int Id { get; set; }
       
        public decimal Deposit { get; set; }
        public string DescriptionGift { get; set; }
       
        public DateTime DepositDate { get; set; }
        
        [JsonIgnore]
        public Project Project { get; set; }
        [JsonIgnore]
        public User User  { get; set; }//backer

    }
}
