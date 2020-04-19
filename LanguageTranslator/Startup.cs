using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LanguageTranslator.Data;
using LanguageTranslator.Data.Repositories;
using LanguageTranslator.Interfaces;
using LanguageTranslator.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LanguageTranslator
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            using (SqlConnection connection = new SqlConnection(Configuration["ConnectionStrings:DefaultConnection"]))
            {
                string sqlExpression = "SELECT * FROM RU_EN";
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Words.translates.AddLast(new TranslateWord
                        {
                            Id = reader.GetInt32(0),
                            Word = reader.GetString(1),
                            Translate = reader.GetString(2)
                        });
                    }
                    connection.Close();

                    Words.translates.OrderBy(w => w.Word);
                }
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddTransient<IDataSaver, DatabaseSaver>();
            services.AddTransient<ITranslates, TranslateRepository>();
            services.AddTransient<ISearch, SearchRepository>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

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
