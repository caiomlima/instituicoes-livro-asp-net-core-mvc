using Capitulo2.Data;
using Capitulo2.Models.Infra;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capitulo2 {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<IESContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IESConnection"))); // Entity / DbContext
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddDbContext<IESContext>();
            services.AddIdentity<UsuarioDoApp, IdentityRole>()
                .AddEntityFrameworkStores<IESContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/Infra/Acessar";
                options.AccessDeniedPath = "/Infra/AcessoNegado";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages("/Home/Error", "?statusCode={0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "areaRoute",
                    pattern: "{area:exists}/{controller}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database = IESCasaDoCodigo; Trusted_Connection = True; MultipleActiveResultSets = true");
        //}

    }
}
