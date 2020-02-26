using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowDo.Core.Data;
using CrowDo.Models;
using CrowDo.Options;
using CrowDo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CrowDo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrowDoController : ControllerBase
    {
        //private readonly IExcelIO _excelIO;
        private readonly ILogger<CrowDoController> _logger;
        private readonly IProjectService _projectService;
        private readonly IFundingPackageService _fundingPackageService;
        private readonly IUserService _userService;
        private readonly IReportService _reportService;
        public CrowDoController(ILogger<CrowDoController> logger,
            //IExcelIO excelIO,
            IFundingPackageService fundingPackageService,
            IProjectService projectService,
            IUserService userService,
            IReportService reportService)
        {
            _logger = logger;
           //_excelIO = excelIO;
            _projectService = projectService;
            _fundingPackageService = fundingPackageService;
            _userService = userService;
            _reportService = reportService;
        }



        [HttpGet]
        public String Hello()
        {
            return "Code girls welcome you!";
        }


        //[HttpGet("users / excel /{filename}")]
        //public List<User> GetCustomersFromExcel([FromRoute] string fileName)
        //{
        //    var excelIO = new ExcelIO(new CrowDoDbContext());
        //    return excelIO.ReadExcel(fileName);
        //}

        [HttpGet("users")]
        public List<User> GetUsers()
        {
            return _userService.GetUsers();
        }

        [HttpGet("user/{id}")]
        public User GetUserById(int id)
        {
            return _userService.GetUserById(id);
        }

        [HttpPost("user")]
        public User CreateUser(
            [FromBody] CreateUserOptions options)
        {
           return _userService.CreateUser(options);
        }

        [HttpPut("user/{id}")]
        public User UpdateUser([FromRoute] int id,
            [FromBody] UpdateUserOptions options)
        {
            return _userService.UpdateUser(id, options);
        }

        //////Project

        [HttpGet("projects")]
        public List<Project> GetProjects()
        {
            return _projectService.GetProjects();
        }

        [HttpGet("project/{id}")]
        public Project GetProjectById(int id)
        {
            return _projectService.GetProjectById(id);
        }

        [HttpPost("project")]
        public Project CreateProject(
            [FromBody] CreateProjectOptions options)
        {
            return _projectService.CreateProject(options);
        }

        [HttpPut("project/{id}")]
        public Project UpdateProject([FromRoute] int id,
            [FromBody] UpdateProjectOptions options)
        {
            return _projectService.UpdateProject(id, options);
        }

        //creator's projects
        [HttpGet("projects/user/{userId}")]
        public List<Project> SearchProductsByUserId([FromRoute] int userId)
        {
            return _projectService.SearchProductsByUserId(userId);
        }

        ////FundingPackage

        [HttpGet("fundingpackages/project/{projectId}")]
        public List<FundingPackage> GetFundingPackagesByProjectId([FromRoute] int projectId  )
        {
            return _fundingPackageService.SearchFundingPackages(projectId);
        }
        //baker chooses a fund
        [HttpPut("fundingpackage/user/{userid}/package/{packageId}")]
        public FundingPackage Fund(  int userId,       int packageId)
        {
            return _fundingPackageService.Fund(userId, packageId);
        }

        //////Reports
        [HttpGet("report/topcreators")]
        public List<User> Top20ProjectCreators()
        {
            return _reportService.Top20ProjectCreators();
        }
        [HttpGet("report/topprojects")]
        public List<Project> TopProjects()
        {
            return _reportService.TopProjects();
        }
    }
}
    
