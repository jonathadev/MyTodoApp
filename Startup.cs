using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using MyTodoApp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection; // aqui a dananda could not get the reflection type for DbContext : MyTodoApp.Data.ApplicationDbContext
using System.Globalization;
using Microsoft.AspNetCore.Localization;


//using Microsoft.AspNetCore.Mvc.Localization;


//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

namespace MyTodoApp
{
    public class Startup
    {
          /*
       dois?
       
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

    
       
        public Startup(IConfiguration configuration) 
        {
            this.Configuration = configuration;
               
        }*/
        public Startup(IConfiguration configuration) 
        {
            this.Configuration = configuration;
               
        }
                        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")));//DefaultConnection
            
            
/*Solucao felipe
             services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));*/

/*services.AddDbContext<AspDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("optimumDB")));
    
    https://stackoverflow.com/questions/43098065/entity-framework-core-dbcontextoptionsbuilder-does-not-contain-a-definition-for
    */
            
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
        }



/*
$ dotnet-aspnet-codegenerator controller -name TodoController -dc ApplicationDbContext -m Todo --useDefaultLayout --useSqlite --referenceScriptLib
raries
Building project ...
Finding the generator 'controller'...
Running the generator 'controller'...
Specify --help for a list of available options and commands.
Unrecognized option '--useSqlite'
   at Microsoft.Extensions.CommandLineUtils.CommandLineApplication.HandleUnexpectedArg(CommandLineApplication command, String[] args, Int32 index,
String argTypeName, Boolean ignoreContinueAfterUnexpectedArg)
   at Microsoft.Extensions.CommandLineUtils.CommandLineApplication.Execute(String[] args)
   at Microsoft.VisualStudio.Web.CodeGeneration.ActionInvoker.Execute(String[] args)
   at Microsoft.VisualStudio.Web.CodeGeneration.CodeGenCommand.Execute(String[] args)

services.AddDbContext<MvcCallContext>(options =>
    options.UseSqlite(Configuration.GetConnectionString("MvcMovieContext")));
    
    https://stackoverflow.com/questions/58495605/ef-and-asp-net-core-cannot-generate-a-controller
    */



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

/*
            var supportedCultures = new[] {new CultureInfo("pt-BR")};
            app.UseRequestLocalization(new UseRequestLocalizationOptions
            {   
                DefaultRequestCultures = new RequestCulture("pt-BR", "pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures =supportedCultures
                
            });
*/

// Definindo a cultura padrão: pt-BR
            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
