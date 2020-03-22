using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fw.GA.StatusMonitor.Infrastructure.GroupAlarmApi;
using FW.GA.StatusMonitor.Core.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace FW.GroupAlarm.StatusMonitor
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
            services.AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/");
                });

            var y = Configuration
                    .GetSection("ACLMappings")
                    .GetChildren()
                    .Select(c => (c.Key, c.Value))
                    .ToList();

            services.AddSingleton(
                (IOrganizationService)new OrganisationService(
                        webServiceBaseUrl: Configuration.GetValue<string>("GroupAlarmApi:BaseUrl"),
                        webApiKey: Configuration.GetValue<string>("GroupAlarmApi:OrganizationApiKey"),
                        personalAccessToken: Configuration.GetValue<string>("GroupAlarmApi:PersonalAccessToken")
                    ));

            services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
                .AddAzureAD(options => Configuration.Bind("AzureAd", options));

            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "DashboardAccess",
                    policy => policy.RequireClaim("ACL_FwStatusDashboard"));
                foreach (var x in y)
                {
                    options.AddPolicy(x.Key, policy => policy.RequireClaim(x.Value));
                }
            });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints
                    .MapRazorPages()
                    .RequireAuthorization();
            });
        }
    }
}
