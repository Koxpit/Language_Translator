using LanguageTranslator.Data;
using LanguageTranslator.Data.Repositories;
using LanguageTranslator.Enums;
using LanguageTranslator.Interfaces;
using LanguageTranslator.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace LanguageTranslator
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public static string ConnectionString { get; set; }

        public static List<Language> Langs { get; private set; } = new List<Language>
            {
                new Language
                {
                    Id = 1,
                    Name = "Russian",
                    UniCode = "[\u0400-\u04FF]+",
                    Acronym = "RU"
                },
                new Language
                {
                    Id = 2,
                    Name = "English",
                    UniCode = "[\u0000-\u007F]+",
                    Acronym = "EN"
                }
            };

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddTransient<IDataSaver, FileSaver>();
            services.AddTransient<ITranslates, TranslateRepository>();
            services.AddTransient<ISearch, SearchRepository>();
            services.AddTransient<IInitializer, FileInitializer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IInitializer initializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            ConnectionString = Configuration["ConnectionStrings:DefaultConnection"];
            initializer.Initialize();

            app.UseRouting();

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
