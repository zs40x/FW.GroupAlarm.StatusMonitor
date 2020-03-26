using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fw.GA.StatusMonitor.Infrastructure.Authorization;
using Fw.GA.StatusMonitor.Infrastructure.GroupAlarmApi;
using FW.GA.StatusMonitor.Core.Interfaces;
using FW.GA.StatusMonitor.Core.ValueTypes.DTO.Authorization;
using FW.GA.StatusMonitor.Logic.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

            services.AddSingleton(
                (IUnitAuthorizationService)new UnitAuthorizationService(
                    GetAuthorizationMappings()));
            services.AddSingleton(
                (IOrganizationDataService)new OrganizationDataService(
                        webServiceBaseUrl: Configuration.GetValue<string>("GroupAlarmApi:BaseUrl"),
                        webApiKey: Configuration.GetValue<string>("GroupAlarmApi:OrganizationApiKey"),
                        personalAccessToken: Configuration.GetValue<string>("GroupAlarmApi:PersonalAccessToken")
                    ));
            services.AddScoped<IOrganizationStatusService, OrganizationStatusService>();

            services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
                    .AddAzureAD(options => Configuration.Bind("AzureAd", options));
        }

        private List<AuthorizationMapping> GetAuthorizationMappings()
        {
            return Configuration
                                .GetSection("ACLMappings")
                                .GetChildren()
                                .Select(c => new AuthorizationMapping
                                {
                                    UnitId = int.Parse(c.Key),
                                    AdGroupObjectId = c.Value
                                })
                                .ToList();
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
                endpoints.MapGet("/logout", context =>
                {
                    context.SignOutAsync().GetAwaiter().GetResult();
                    context.Response.Redirect("/");
                    return context.Response.WriteAsync("Logged out");
                });
                endpoints
                    .MapRazorPages()
                    .RequireAuthorization();
            });
        }
    }
}
