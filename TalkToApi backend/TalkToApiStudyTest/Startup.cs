using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TalkToApiStudyTest.Database;
using TalkToApiStudyTest.Helpers;
using TalkToApiStudyTest.Hub;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Repositories;
using TalkToApiStudyTest.V1.Repositories.Contracts;

namespace TalkToApiStudyTest
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


            services.AddCors(
                cfg =>
                {
                    cfg.AddPolicy("anyMethod", policy =>
                    {
                        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");

                        policy.AllowAnyHeader()
                      .AllowAnyMethod()
                      .SetIsOriginAllowed((host) => true)
                      .AllowCredentials();

                    });


                });
           


            services.AddDbContext<TalkToContext>(options => options.UseSqlServer
            (Configuration.GetConnectionString("DefaultConnection")));


            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            });


            services.AddDbContext<TalkToContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                });


            services.AddIdentity<ApplicationUser, IdentityRole>(
                config =>
                {
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                }
                           
                ) 
                .AddEntityFrameworkStores<TalkToContext>()
                .AddDefaultTokenProviders();


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();


            services.AddControllers(config =>
            {
                config.ReturnHttpNotAcceptable = true;
                config.InputFormatters.Add(new XmlSerializerInputFormatter(config));
                config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            }).AddNewtonsoftJson(
                opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });



            services.AddAuthentication(options =>
            {

                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(
         //indicate which elements of token must be validated
         options => options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
         {

             ValidateIssuer = false,
             ValidateAudience = false,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("chave-api-jwt-minhas-tarefas"))
         });



            services.AddApiVersioning(
                cfg =>
                {
                    cfg.ReportApiVersions = true;
                    cfg.ApiVersionReader = new HeaderApiVersionReader("api-version");
                    cfg.AssumeDefaultVersionWhenUnspecified = true;
                    cfg.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Description = "Please insert JWT token into field"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                        }
                    });


                c.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "Talk to -  API v1.0"
                });


                var caminhoProjeto = PlatformServices.Default.Application.ApplicationBasePath;
                var NomeProjeto = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var caminhoArquivoXMLComentario = Path.Combine(caminhoProjeto, NomeProjeto);

                c.IncludeXmlComments(caminhoArquivoXMLComentario);

                c.DocInclusionPredicate((version, desc) =>
                {
                    if (!desc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;
                    var versions = methodInfo.DeclaringType.GetCustomAttributes(true).OfType<ApiVersionAttribute>().SelectMany(attr => attr.Versions);
                    var maps = methodInfo.GetCustomAttributes(true).OfType<MapToApiVersionAttribute>().SelectMany(attr => attr.Versions).ToArray();
                    version = version.Replace("v", "");
                    return versions.Any(v => v.ToString() == version && maps.Any(v => v.ToString() == version));
                });

                c.ResolveConflictingActions(apiDescription => apiDescription.First());




            });


            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                                         .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                                         .RequireAuthenticatedUser().Build());
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });


            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DTOMapperProfile());
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);


            services.AddSignalR();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

          

            app.UseAuthentication();

           

            app.UseRouting();



           app.UseCors("anyMethod");


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapHub<BroadcastHub>("/notify");
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(cfg =>
            {
                cfg.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Talk ToAPI v1.0");

                cfg.RoutePrefix = string.Empty;
            });
        }
    }
}
