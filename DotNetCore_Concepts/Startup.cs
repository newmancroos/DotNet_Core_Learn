using DotNetCore_Concepts.Entities;
using DotNetCore_Concepts.Repoitories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNetCore_Concepts
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        //Register all your services
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(option => option.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));
            services.AddScoped<IRepository<Employee>, Repository<Employee>>();

            //Added to use Controller
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                //DeveloperExceptionPageOptions options = new DeveloperExceptionPageOptions
                //{
                //    SourceCodeLineCount = 5 //this will display 5 lines before and after the line which exception happened
                //};
                //app.UseDeveloperExceptionPage(options);
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World");
            //    });
            //});

            //app.Use(async (context, next) => {
            //    //await context.Response.WriteAsync("My first middleware");
            //    logger.LogInformation("MW1 : Incoming Message");
            //    await next();
            //    logger.LogInformation("MW1 : Out going Message");
            //});

            //app.Use(async (context, next) => {
            //    //await context.Response.WriteAsync("My first middleware");
            //    logger.LogInformation("MW2 : Incoming Message");
            //    await next();
            //    logger.LogInformation("MW2 : Out going Message");
            //});

            ////This will change the default file from index.html to home.html
            //DefaultFilesOptions defaultFileOption = new DefaultFilesOptions();
            //defaultFileOption.DefaultFileNames.Clear();
            //defaultFileOption.DefaultFileNames.Add("home.html");
            //app.UseDefaultFiles(defaultFileOption);
            ////--------------------------------------------------------------

            ////-----------------------------------------------------------------------
            //app.UseDefaultFiles(); //display default.html page default. UseDefaultFiles and UseStaticFile middlewares should be in correct order
            //app.UseStaticFiles(); //This midleware enables displaying static file like jpg, html etc. Static file with wwwroot will be display as root url 
            ////If you want to display image in wwwroot -> images we need to specify http://localhost:3434/images/image.jpg
            ////--------------------------------------------------------------------------

            ////Instead of using app.UseDefaultFiles(), app.UseStaticFiles() we can use UseFileServer() method
            ////and we can also change the default file using FileservierOptions
            //FileServerOptions options = new FileServerOptions();
            //options.DefaultFilesOptions.DefaultFileNames.Clear();
            //options.DefaultFilesOptions.DefaultFileNames.Add("home.html");
            //app.UseFileServer(options);

            //app.UseFileServer();
            //-----------------------------------------------------------------------------
            ////app.UseStaticFiles();
            ////app.Run(async (context) =>
            ////{
            ////    await context.Response.WriteAsync($"Hosting Environment: {env.EnvironmentName}");
            ////   // throw new System.Exception("Some error processing the request"); //This exception will be thrown if app.UserFileServer not find any file requested.
            ////    //await context.Response.WriteAsync("MW3 : Request handled and response produces");
            ////});

            //////Thismiddle ware is not reachable
            ////app.Run(async (context) =>
            ////{
            ////    //Exception handling
            ////    await context.Response.WriteAsync("My third middleware");
            ////});
            //------------------------------------------------------------------------------
            
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
