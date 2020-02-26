using CrowDo.Core.Data;
using CrowDo.Models;
using CrowDo.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CrowDo.Services
{
    public class ProjectService : IProjectService
    {
        private readonly CrowDoDbContext context_;
        private readonly IUserService user_;

        public ProjectService(
            CrowDoDbContext context,
            IUserService users)
        {
            context_ = context;
            user_ = users;
        }

        public Project CreateProject(CreateProjectOptions projectOptions)
        {
            if (projectOptions == null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(projectOptions.Title) ||
                projectOptions.Budget <= 0 ||
                string.IsNullOrWhiteSpace(projectOptions.Description) ||
                projectOptions.UserId == 0)
            {
                return null;
            }

            var project = new Project()
            {
                Title = projectOptions.Title,
                Budget = projectOptions.Budget,
                Description = projectOptions.Description,
                Category = ProjectCategoryUti.GetCategory( projectOptions.Category),
                CreationDate = DateTime.Now,
                User = context_.Set<User>().Find(projectOptions.UserId) 
                 
            };
            context_.Add(project);
            context_.SaveChanges();

            if (projectOptions.FundingPackages.Count == 0)
            {
                projectOptions.FundingPackages = new List<string>() {
                    "funding package 1"
                };
            }

            //if title is not unique
            try
            {
                foreach (string package in projectOptions.FundingPackages)
                {
                    var fundingPackage = new FundingPackage
                    {
                        Deposit = 1000,
                        DescriptionGift = package,
                        Project = project

                    };

                    context_.Set<FundingPackage>().Add(fundingPackage);
                }
                //context_.Set<Project>().Add(project);
                context_.SaveChanges();
            }
            catch (Exception)
            {
                return null;
            }

            context_.SaveChanges();
            return new Project
            {
                Id = project.Id,
                Budget = project.Budget,
                Category= project.Category,
                CreationDate = project.CreationDate,
                Description = project.Description,
                StatusProject= project.StatusProject,
                Title= project.Title
            };  
         
        }

        public List<Project> SearchProject(
         SearchProjectOptions projectOptions)
        {
            if (projectOptions == null)
            {
                return null;
            }

            if (projectOptions.ProjectId == null
                && projectOptions.UserId == null)
            {
                return null;
            }

            var query = context_
                 .Set<Project>()
                 .AsQueryable();

            if (projectOptions.UserId != 0)
            {
                query = query.Where(
                    c => c.User.Id == projectOptions.UserId);
            }

            if (projectOptions.ProjectId != 0)
            {
                query = query.Where(
                    c => c.Id == projectOptions.ProjectId);
            }

            if (!string.IsNullOrWhiteSpace(projectOptions.Description))
            {
                query = query
                      .Where(c => c.Description.Contains(projectOptions.Description));
            }

            if (!string.IsNullOrWhiteSpace(projectOptions.Title))
            {
                query = query
                      .Where(c => c.Title.Contains(projectOptions.Title));
            }
            //to be checked
            if (projectOptions.StatusProject == StatusProject.Available)
            {
                query = query
                      .Where(c => c.StatusProject.Equals(projectOptions.StatusProject));
            }

            if (!projectOptions.Category.Equals(ProjectCategory.Invalid))
            {
                query = query
                     .Where(c => c.StatusProject.Equals(projectOptions.StatusProject));
            }

            return query.ToList();
        }

        public Project UpdateProject(
            int projectId, UpdateProjectOptions projectOptions)
        {
            if (projectOptions == null)
            {
                return null;
            }
            var project = GetProjectById(projectId);

            if (project == null)
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(projectOptions.Description))
            {
                project.Description = projectOptions.Description;
            }

            if (!string.IsNullOrWhiteSpace(projectOptions.Title))
            {
                project.Title = projectOptions.Title;
            }
                        
            context_.SaveChanges();
            return project;
        }

        public Project GetProjectById(int id)
        {
            var project = context_
                .Set<Project>()
                .SingleOrDefault(p => p.Id == id);
            if (project == null)
            {
                return null;
            }
            return project;
        }

        public List<Project> GetProjects()
        {
            return context_.Set<Project>()
                    .ToList();
        }

        public List<Project> SearchProductsByUserId(int userId)
        {
            var query = context_
                .Set<Project>()
                .AsQueryable();

            var userResult = user_.GetUserById(userId);
            if (userResult.Id != 0)
            {
                query = query.Where(
                    c => c.User.Id == userResult.Id);
            }

            return query.Select(item => new Project
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                FundingPackages = item.FundingPackages,
                 
            }).ToList();
        }

    }
}