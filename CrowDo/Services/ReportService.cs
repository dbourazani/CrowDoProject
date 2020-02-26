using CrowDo.Core.Data;
using CrowDo.Models;
using CrowDo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CrowDo
{
    public class ReportService : IReportService
    {
        private readonly CrowDoDbContext context_;
        private readonly IUserService user_;
        private readonly IProjectService project_;
        public ReportService(
            CrowDoDbContext context,
            IUserService users,
            IProjectService projects)
        {
            context_ = context;
            user_ = users;
            project_ = projects;
        }
        public List<User> Top20ProjectCreators()
        {
            var query = context_.Set<User>()
                .AsQueryable();
            var projects = query.Select(
                u => u.Projects)
                .Count();
            var top20 = query.Take(20);
            return top20.ToList();
        }
        public List<Project> TopProjects()
        {
            var limit = DateTime.Now.AddDays(-7);
            var query = context_.Set<Project>()
               .AsQueryable();
            var topProjects = query.Where(c => c.CreationDate >= limit);
            return topProjects.ToList();
        }
    }
}