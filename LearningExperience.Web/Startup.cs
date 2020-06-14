using System;
using LearningExperience.Core.Interfaces;
using LearningExperience.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace LearningExperience.Web
{
    public sealed class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<IDocumentsSchemeService, DocumentsSchemeService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Error");

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
                                   {
                                       FileProvider = new PhysicalFileProvider(Configuration["DocumentsSchemeFolder"]
                                                                               ?? throw new NullReferenceException("Configuration key - DocumentsSchemeFolder doesn't exist")),
                                       RequestPath = "/Documents"
                                   });
            app.UseMvc();
        }
    }
}
