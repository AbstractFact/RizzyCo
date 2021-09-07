using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using BussinesLogic.Services;
using DataAccess;
using Domain;
using Repository;
using BussinesLogic.Helpers;
using BussinesLogic.Authentication;
using BussinesLogic.Messaging;
using BussinesLogic.Services.Strategy;

namespace RizzyCoBE
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
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                services.AddDbContext<RizzyCoContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("RizzyCoProd"));
                });
            }
            else
            {
                services.AddDbContext<RizzyCoContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("RizzyCo"));
                });
            }

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            
            services.AddTransient<IUserAuthService,UserAuthService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<MissionContext>();
            services.AddScoped<CardService>();
            services.AddScoped<GameService>();
            services.AddScoped<MapService>();
            services.AddScoped<MissionService>();
            services.AddScoped<PlayerColorService>();
            services.AddScoped<PlayerService>();
            services.AddScoped<TerritoryService>();
            services.AddScoped<UserService>();
            services.AddScoped<NeighbourService>();
            services.AddScoped<ContinentService>();
            services.AddScoped<PlayerTerritoryService>();
            services.AddScoped<PlayerCardService>();

            services.BuildServiceProvider().GetService<RizzyCoContext>().Database.Migrate();

            services.AddControllers();

            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });

            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

            services.AddCors(options =>
            {

                options.AddPolicy("CORS", builder =>
                {
                    builder.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed((host) => true).AllowCredentials();
                });
            });

            services.AddMemoryCache();
            services.AddSingleton<MessageHub>(); 
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CORS");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MessageHub>("RizzyCoHub");
            });

        }
    }
}
