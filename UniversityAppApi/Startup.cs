using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using UniversityAppApi.Auth.Factory;
using UniversityAppApi.Models;
using UniversityAppApi.Repositories;
using UniversityAppApi.Auth.Helpers;
using UniversityAppApi.Auth.Models;
using UniversityAppApi.Helpers.Filters;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using UniversityAppApi.ViewModels;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace UniversityAppApi
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;

        private static string SecretKey = Encoding.ASCII.GetString(Convert.FromBase64String("dGhpc0lzTXlQZXJmZWN0U2VjcmV0S2V5"));

        private readonly SymmetricSecurityKey _signInKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            _config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISOptions>(config => { });

            services.AddSingleton(_config);

            services.AddSingleton<IJWTFactory, JWTFactory>();

            services.AddTransient<UniversityContextSeedData>();

            services.AddSingleton<BLLUnitOfWork>();

            services.Configure<JWTIssuerOptions>(config =>
            {
                config.Issuer = _config["JWTIssuerOptions:Issuer"];
                config.Audience = _config["JWTIssuerOptions:Audience"];
                config.SignInCredentials = new SigningCredentials(_signInKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddIdentity<UniversityUserModel, UniversityRoleModel>(config =>
            {
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<UniversityContext>()
            .AddDefaultTokenProviders();

            services.AddTransient<ClaimsPrincipal>(
                s => s.GetService<IHttpContextAccessor>().HttpContext.User);

            services.AddMvc(config =>
            {
                config.Filters.Add(new CorsFilterAuth());
                config.Filters.Add(new CorsFilterRes());
                if (_env.IsProduction())
                {
                    config.Filters.Add(new RequireHttpsAttribute());
                }
                config.RespectBrowserAcceptHeader = true;
            }).AddJsonOptions(config =>
            {
                config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                config.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddDbContext<UniversityContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, UniversityContextSeedData seeder)
        {
            app.UseDeveloperExceptionPage();

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                AuthenticationScheme = "Bearer",
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = _config["JWTIssuerOptions:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _config["JWTIssuerOptions:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _signInKey,
                    RequireExpirationTime = false,
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero
                }
            });

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}"
                );
            });

            app.UseIdentity();

            Mapper.Initialize(config =>
            {
                config.CreateMap<NoteViewModel, NoteModel>().ReverseMap();
                config.CreateMap<ProfesorViewModel, ProfesorViewModel>().ReverseMap();
                config.CreateMap<StudentViewModel, StudentModel>().ReverseMap();
                config.CreateMap<SubjectViewModel, SubjectModel>().ReverseMap();
                config.CreateMap<PrezentaViewModel, PrezentaModel>().ReverseMap();
            });

            seeder.EnsureDbCreated().Wait();

            seeder.EnsureSeedData().Wait();
        }
    }
}
