using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowDo.Core.Data;
using CrowDo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

 namespace CrowDo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. 
        //Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<CrowDoDbContext>();
            //services.AddScoped<IExcelIO,ExcelIO>();

            //Scoped : otan xreiastei na xrhsimopoih8ei
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IFundingPackageService, FundingPackageService>(
                sp => new FundingPackageService(
                    sp.GetService<CrowDoDbContext>(),
                    sp.GetService<IUserService>(),
                    sp.GetService<IProjectService>()));


            //STEP 1 OF 2
            //within Startup.cs  
            // method ConfigureServices
            // add the following code before the end of the method
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();


            //STEP 2 OF 2
            //within Startup.cs  
            // method Configure
            // add the following code before the app.UseEndpoints ...


            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
