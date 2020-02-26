using CrowDo.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowDo.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Budget { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public StatusProject StatusProject { get; set; }
        public ProjectCategory Category { get; set; }
        public User User { get; set; }
        public List<FundingPackage> FundingPackages{get; set;}

        
    }
}
