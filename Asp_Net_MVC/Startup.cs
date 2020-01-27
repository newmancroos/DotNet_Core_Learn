using Asp_Net_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Asp_Net_MVC
{
    public class Startup
    {
        private IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(option => option.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));
            //services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
            services.AddScoped<IEmployeeRepository, SqlEmployeeRepository>();
            //services.AddMvc();

            //Identity related
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<AppDbContext>();

            //services.AddIdentity<IdentityUser, IdentityRole>()
            //        .AddEntityFrameworkStores<AppDbContext>();

            //This way too we can configure Identity options
            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequiredLength = 10;
            //});
            //----------------------------------------------------
            //-----Claim Base Authorization------------------------
            //services.AddAuthorization(options =>
            //    {
            //        options.AddPolicy("DeleteRolePolicy",
            //            policy => policy.RequireClaim("Delete Role")
            //                            .RequireClaim("Create Role"));
            //    });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireClaim("Delete Role"));
              });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("EditRolePolicy",
                    policy => policy.RequireClaim("Edit Role"));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminRolePolicy",
                    policy => policy.RequireRole("Administrator"));
            });
            //------------------------------------------------------

            services.AddMvc(options => {
                ////This is to globally inser Authorize attribute. But good option is use controller level
                //var policy = new AuthorizationPolicyBuilder()
                //                .RequireAuthenticatedUser()
                //                .Build();
                //options.Filters.Add(new AuthorizeFilter(policy));
                options.EnableEndpointRouting = false;
                }).AddXmlSerializerFormatters();
        }

         public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                //app.UseStatusCodePages(); //Displays simple 404 page 
                //app.UseStatusCodePagesWithRedirects("/Error/{0}");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");

                //Both app.UseStatusCodePagesWithRedirects and UseStatusCodePagesWithReExecute are both as a end users.
                // but internally the process of page is diferent.

            }

            app.UseStaticFiles();
            app.UseRouting();
            //app.UseMvc();
            //////All 4 method of Conventional routes will work. Since we use attribute routing some of them is commented
            /*
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                 name: "default",
                 pattern: "{controller=Home}/{action=Index}/{id?}");
            });*/


            //app.UseMvcWithDefaultRoute();

            //Identity related
            app.UseAuthentication();
            //-----------------------------
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
