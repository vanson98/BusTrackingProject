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
using BusTracking.Application.System.Auths;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Runtime.InteropServices.ComTypes;
using BusTracking.BackendApi.HubConfig;
using Microsoft.AspNetCore.SignalR;
using Quartz;
using System.Collections.Specialized;
using Quartz.Impl;
using BusTracking.BackendApi.Quartz;
using Quartz.Spi;
using FluentValidation.AspNetCore;
using BusTracking.ViewModels.Catalog.Buses;
using FluentValidation;

namespace BusTracking.BackendApi
{
    public class Startup
    {
        private const string _defaultCorsPolicyName = "localhost";
        public IConfiguration _appConfiguration { get; }
        public Startup(IConfiguration configuration)
        {
            _appConfiguration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {   
            // 0. Web API
            services.AddControllers().AddFluentValidation(fv=>fv.RegisterValidatorsFromAssemblyContaining<CreateBusRequestValidator>());

            // 1. Config DbContext và DI cho DB context
            services.AddDbContext<BusTrackingDbContext>(options =>
                    options.UseSqlServer(_appConfiguration.GetConnectionString(SystemConstants.MainConnectionString)));

            // 2. Đăng kí và cấu hình Identity sử dụng các service của BusTrackingDbContext
            services.AddIdentity<AppUser, AppRole>()
                    .AddEntityFrameworkStores<BusTrackingDbContext>()
                    .AddDefaultTokenProviders();

            //3. DI Services
            services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IDriverService, DriverService>();
            services.AddTransient<IBusService, BusService>();
            services.AddTransient<IRouteService, RouteService>();
            services.AddTransient<IStopService, StopService>();
            services.AddTransient<IStudentService, StudentService>();

            //services.AddTransient<IValidator<CreateBusRequestDto>, CreateBusRequestValidator>();

            // 4. Cấu hình Authentication và  Authorization 
            services
                .AddAuthorization()
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = _appConfiguration["Tokens:Issuer"],
                        ValidAudience = _appConfiguration["Tokens:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appConfiguration["Tokens:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };

                    // Authen của SignalR
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/bushub")))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            // 5. Configure CORS for angular web (Chỉ cho web)
            services.AddCors(options => options.AddPolicy(
                _defaultCorsPolicyName,
                builder => builder
                    .WithOrigins("http://localhost:3500")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                )
            ); 

            // 6. Cấu hình swagger
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

            // 7. SignalR Hub
            services.AddSignalR();

            // 8. DI Provider for SignalR
            services.AddSingleton<IUserIdProvider, CustomIdProvider>();


            // 10.  Register the hosted service for quartz
            services.AddSingleton<IJobFactory, QuartzJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<UpdateStudentStatusJob>();
            services.AddSingleton(new JobMetadata(Guid.NewGuid(), typeof(UpdateStudentStatusJob), "Change Student Status Job", "0 0 0 * * ?"));
            services.AddHostedService<QuartzHostedService>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable CORS!
            app.UseCors(_defaultCorsPolicyName); 

            // Route
            app.UseRouting();

            // Auth
            app.UseAuthentication();
            app.UseAuthorization();

            // Middleware của swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger BusTracking V1");
            });

            //var idProvider = new CustomIdProvider();
            //GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => idProvider);

            //Endpoint
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute( 
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapHub<BusTrackingHub>("/bushub");
            });
        }
    }
}
