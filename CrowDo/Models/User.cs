using CrowDo.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;



namespace CrowDo.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int? YearOfBirth { get; set; }
        public string Email { get; set; }
        public StatusUser Status { get; set; }
        [JsonIgnore]
        public List<Project> Projects { get; set; }
        public List<FundingPackage> FundingPackages { get; set; } 
        
    }
}
