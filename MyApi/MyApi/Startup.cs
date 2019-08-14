using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyApi.MyContext;
using MyApi.Models;
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using MyApi.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

namespace MyApi
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
            //Inject AppSettings
            services.Configure<AppSettings>(Configuration.GetSection("ApplicationSettings"));


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
               .AddJsonOptions(op =>
               {
                   var resolver = op.SerializerSettings.ContractResolver;
                   if (resolver != null)
                   {
                       (resolver as DefaultContractResolver).NamingStrategy = null;
                   }
               });


            services.AddDbContext<MyDbContext>(op => op.UseSqlServer(Configuration.GetConnectionString("NewsDB")));
            services.AddDefaultIdentity<User>().AddEntityFrameworkStores<MyDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            services.Configure<IdentityOptions>(op =>
            {
                op.Password.RequireDigit = false;
                op.Password.RequireNonAlphanumeric = false;
                op.Password.RequireLowercase = false;
                op.Password.RequireUppercase = false;
                op.Password.RequiredLength = 4;
            }
            );

            //Jwt Authentication

            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });


            services.AddCors(o => o.AddPolicy("AllowOrigin", builder =>
            {
                builder.WithOrigins(Configuration["ApplicationSettings:Client_URL"].ToString())
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            }));

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

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseMvc();


        }
    }
}
