using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BaseInMemoryArchitecture.BusinessLogic;
using BaseInMemoryArchitecture.Common;
using BaseInMemoryArchitecture.Web.Common;

namespace BaseInMemoryArchitecture.Web
{
    public class Startup
    {
        private readonly string _myCorsPolicy = "MyCorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Cors
            services.AddCors(options =>
            {
                options.AddPolicy(name: _myCorsPolicy, options =>
                {
                    options.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
            });

            //JWT Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.Client, Policies.ClientPolicy());
            });

            services.AddControllers();

            //Extensions
            services.AddAutoMapper(typeof(Startup));
            services.AddMemoryCache();
            services.RegisterApplicationCommon();
            services.RegisterApplicationServices();

            //Swagger
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(name: "v1", new OpenApiInfo
                {
                    Title = "In-Memory Cache API",
                    Description = "Base Architecture API with In-Memory Cache and JWT Token Authentication",
                    Version = "v1",
                    Contact = new OpenApiContact()
                    {
                        Name = "Juan Azar",
                        Url = new Uri("https://github.com/JuanAzar")
                    }
                });

                x.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = @"JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the input field below.
                                      <br><br>
                                      Example: 'Bearer 12345abcdef'
                                      <br><br>
                                      Note: for authentication use 'john@doe.com' and '123456'
                                      <br><br>",
                        Name = "Authorization",
                        Scheme = "bearer",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme ="oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });                
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //Use Swagger
            app.UseSwagger();

            //Use Swagger UI
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "In-Memory Cache API");
                x.DocumentTitle = "In-Memory Cache API Swagger UI";
                x.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            //Use Cors
            app.UseCors(_myCorsPolicy);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
