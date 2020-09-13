using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BusTracking.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BusTracking.Utilities.Constants;
using BusTracking.Data.Entities;
using Microsoft.AspNetCore.Identity;
using BusTracking.Application.System.Users;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using BusTracking.Application.Catalog.DriverService;
using BusTracking.Application.Catalog.BusService;
using BusTracking.Application.Catalog.RouteService;
using BusTracking.Application.Catalog.StudentService;
using BusTracking.Application.Catalog.StopService;

namespace BusTracking.BackendApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // Config DbContext và DI cho DB context
            services.AddDbContext<BusTrackingDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString(SystemConstants.MainConnectionString)));

            // Cấu hình Identity 
            services.AddIdentity<AppUser, AppRole>()
                    .AddEntityFrameworkStores<BusTrackingDbContext>()
                    .AddDefaultTokenProviders();

            // DI Services
            services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IDriverService, DriverService>();
            services.AddTransient<IBusService, BusService>();
            services.AddTransient<IRouteService, RouteService>();
            services.AddTransient<IStopService, StopService>();
            services.AddTransient<IStudentService, StudentService>();

            // Cấu hình swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger Bus Tracking", Version = "v1" });
                // Cấu hình security cho swagger
                // Khi call api thì sẽ truyền 1 header tên là Authorization, có kiểu là ApiKey
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                // Yêu cầu khi gọi swagger phải truyền vào header một bearer
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });

        }
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();

            app.UseAuthorization();

            // Middleware của swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger BusTracking V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute( 
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
