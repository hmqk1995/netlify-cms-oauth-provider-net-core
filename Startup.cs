using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AspNet.Security.OAuth.GitHub;

namespace netlify_cms_oauth_provider_aspnet_core
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
        services.Configure<CookiePolicyOptions>(options =>
        {
            // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });

        // Add Cookie settings
        services.AddAuthentication(options =>
        {
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })        
        // Add Cookie settings
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.LoginPath = "/account/login";
            options.LogoutPath = "/account/logout";
            options.SlidingExpiration = true;
        })
        // Add GitHub authentication
        .AddGitHub("Github", options =>
        {
            options.ClientId = "01de9e7e7c537ded06ae"; // client id from registering github app
            options.ClientSecret = "c88495e2a0902d401ce395eb3001a8c99ad64138"; // client secret from registering github app
            options.CallbackPath = "/callback";
            options.Scope.Add("user:email"); // add additional scope to obtain email address
        });
        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
        if (env.IsDevelopment())
        {
        app.UseDeveloperExceptionPage();
        }
        else
        {
        app.UseHsts();
        }

        // Enable authentication
        app.UseCookiePolicy();
        app.UseAuthentication();

        app.UseHttpsRedirection();
        app.UseMvc();
        }
    }
}
