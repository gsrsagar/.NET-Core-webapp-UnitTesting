using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SocialMediaLinkedIn.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace SocialMediaLinkedIn
{
    public class Startup
    {

        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
           
        
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(
               options => options.UseSqlServer(_config.GetConnectionString("EmployeeDbConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<AppDbContext>();
            services.AddMvc(options =>
            {
                AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
                      .RequireAuthenticatedUser()
                      .Build();
                options.Filters.Add(new AuthorizeFilter(policy));

            });
            services.ConfigureApplicationCookie(options =>
            {
                //cookie settings
                options.Cookie.HttpOnly = true;
                //option.Cookie.Expiration=TImeSpan.FromDays(10)
                options.ExpireTimeSpan = TimeSpan.FromSeconds(600);
                options.LoginPath = "/Account/Login"; //If loginpath not set here then asp.net core will default to /Account/Login
                options.LogoutPath = "/Account/Logout"; //If loginpath not set here then asp.net core will default to /Account/Logout
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
           /* services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromSeconds(600);
            });*/
            /*services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;

            });*/
            services.AddMvc(options => options.EnableEndpointRouting = false).AddXmlSerializerFormatters();
       
            services.AddTransient<EmployeeRepository, SQLEmployeeRepositorycs>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSession();
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {       
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
            }
            
            /* else if
                 (env.IsStaging() || env.IsProduction() || env.IsEnvironment("UAT"))
             {
                 app.UseExceptionHandler("/Error");
             }*/
             /*DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
             defaultFilesOptions.DefaultFileNames.Clear();
             defaultFilesOptions.DefaultFileNames.Add("foo.html");
             app.UseDefaultFiles(defaultFilesOptions);*/
             app.UseStaticFiles();
             app.UseSession();
             app.UseAuthentication();
            // app.UseMvcWithDefaultRoute();

            /*app.UseRouting();
           app.UseEndpoints(endpoints =>
           {
               endpoints.MapControllers();
           }
           );*/
            /* FileServerOptions fileserverOptions = new FileServerOptions();
             fileserverOptions.DefaultFilesOptions.DefaultFileNames.Clear();
             fileserverOptions.DefaultFilesOptions.DefaultFileNames.Add("foo.html");
             app.UseFileServer(fileserverOptions); */

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=account}/{action=Login}");
            });

            

            //sending http context object having response object
            //this is a middleware that first middleware calls and it becomes terminal middleware and all other middlewares
            //won't run 
            
           /* app.Run(async (context) =>
            {
                await context.Response.WriteAsync(" Hello from middleware");
              
            });
*/





        }
    }
}
