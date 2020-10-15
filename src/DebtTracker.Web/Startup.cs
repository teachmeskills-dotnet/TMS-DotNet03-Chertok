using DebtTracker.BLL.Repository;
using DebtTracker.BLL.Services;
using DebtTracker.Common.Interfaces;
using DebtTracker.DAL.Context;
using DebtTracker.DAL.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace DebtTracker.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IEmailService, EmailService>();
            services.AddDbContext<DebtTrackerContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DebtTrackerDatabase")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<DebtTrackerContext>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();    // add autentification
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
