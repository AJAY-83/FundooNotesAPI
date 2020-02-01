using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectionBusinessLayer.ElectionBLService;
using ElectionBusinessLayer.IElectionBL;
using ElectionRepositoryLayer.Context;
using ElectionRepositoryLayer.ElectionRLServices;
using ElectionRepositoryLayer.IElectionRL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace ElectionService
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
            services.AddTransient<IVoterBusinessLayer, VoterBusinessLayer>();
            services.AddTransient<IVoterRL, VoterRLServices>();

            services.AddTransient<IConsituency, ConsituencyBLServices>();
            services.AddTransient<IConsituencyRL, ConsituencyRLServices>();

            services.AddTransient<IPartyBL, PartyBLServices>();
            services.AddTransient<IPartyRL, PartyRLServices>();

            services.AddTransient<ICandidateBL, CandidateBLServices>();
            services.AddTransient<ICandidateRL, CandidateRLServices>();

            services.AddDbContext<AuthenticationContext>(opts => opts.UseSqlServer(Configuration["ConnectionStrings:IdentityConnection"]));
            var key = Encoding.UTF8.GetBytes(Configuration["SecretKey:Key"]);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })



             
         //// JWT(Json Web Token) it is use to Authenticate the user
         .AddJwtBearer(options =>
         {
             options.RequireHttpsMetadata = false;
             options.SaveToken = false;
             options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
             {
                 ValidateIssuerSigningKey = true,
                 IssuerSigningKey = new SymmetricSecurityKey(key),
                 ValidateIssuer = false,
                 ValidateAudience = false,
                 ClockSkew = TimeSpan.Zero,
             };
         });

            //// Here have the CORS(Cross Origin Resource Sharing) 
            services.AddCors(options =>
            {
                options.AddPolicy("Access-Control-Allow-Origin",
                    builder => builder.WithOrigins("*")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .AllowAnyOrigin());
            });

            //// here have the Swagger ... swagger just expose the our api 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new Info { Title = "My Fundoo API", Version = "v1.0", Description = "fundoo" });
                //   c.OperationFilter<FileUploadedOperation>(); ////Register File Upload Operation Filter
                //c.DescribeAllEnumsAsStrings();
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                //// here have the Authentication related code
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
             {
               { "Bearer", new string[] {} }
             });
            });




            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "My Fundoo API (V 1.0)");
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
