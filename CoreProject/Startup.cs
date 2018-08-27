using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CoreProject.Data;
using CoreProject.Services;
using CoreProject.Middlewares;
using Microsoft.AspNetCore.Rewrite;

namespace CoreProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MyConStr")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Account/Manage");
                    options.Conventions.AuthorizePage("/Account/Logout");
                    //options.Conventions.AllowAnonymousToPage("/CountryCrud");
                    //options.Conventions.AllowAnonymousToFolder("/Countries");
                });

            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            services.AddSingleton<IEmailSender, EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) // if I'm working in Development enviroment 
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage(); // to see errors which developers understands this error
                app.UseDatabaseErrorPage();
            }
            else // if I'm working in other enviroments like production or project 
            {
                app.UseExceptionHandler("/Error");
            }

            ////*************** To use My Own Middleware   
            //app.UseMiddleware<MyMiddleware>();
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hiii");
            //});
            //******************************


            // **********How to use My Own Middleware without write the name of the class just write app.useAhmed 
            //1- add extension class 
            //2- change the namespace to Microsoft.AspNetCore.Builder
            //3- see the definition of app.useMvc to make it like it 
            //4- app.useMvc() => Go to Definition
            //5- see the code in this class 
            //app.UseAhmedSalem();
            //******************************


            //**** To Secure My Data*******
            // 1- Right click Properties on the project => Debug => Copey https and paste in app URL
            //2- Write the following:
            // told him to make a middleware to rewrite request 
            // any Http request convert it to https request
            app.UseRewriter(new RewriteOptions().AddRedirectToHttpsPermanent());
            //******************************


            app.UseStaticFiles();

            app.UseAuthentication(); // Instead of app.UseIdentity()

            app.UseMvc();
        }
    }
}
