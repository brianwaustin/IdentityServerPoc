using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Validation;
//using IdentityServerPoc.Infrastructure.Security;
//using IdentityServerPoc.Infrastructure.Security.Entities;
//using IdentityServerPoc.Infrastructure.Security.Interfaces;
//using IdentityServerPoc.Infrastructure.Security.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace IdentityServerPoc
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddSingleton<IPostConfigureOptions<CustomAuthenticationOptions>, ApplicationKeyAuthenticationPostConfigureOptions>();
            //services.AddTransient<IExtensionGrantValidator, ApplicationKeyGrantValidator>();

            /* Configure identity server with in-memory stores, keys, clients and scopes */
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddTestUsers(Config.GetUsers());

            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();            

            services.AddAuthentication("Bearer")
             .AddIdentityServerAuthentication(options =>
             {
                 options.Authority = "http://localhost:5001";
                 options.RequireHttpsMetadata = false;

                 options.ApiName = "api1";
             });

            /* Custom authentication header */
            //services.AddAuthentication(CustomAuthenticationDefaults.AuthenticationSchemeAppKey)
            //    .AddAppKey<ApplicationKeyAuthenticationService>(o =>
            //    {
            //        o.Realm = "api1";
            //    });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            /* Registers the IdentityServer services in DI. It also registers an in-memory store for runtime state. 
               Useful to get started, but needs to be replaced by some persistent key material for production scenarios. */
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseMvc();
            
        }
    }
}
