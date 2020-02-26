using CrowDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CrowDo.Services
{
    public interface IReportService
    {
        public List<Project> TopProjects();
        public List<User> Top20ProjectCreators();
    }
}