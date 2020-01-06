

namespace RegistrationApplication
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using BusinessLayer.Services;
    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication.Facebook;
    using Microsoft.AspNetCore.Authentication.Google;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authentication.OAuth;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.JsonPatch.Operations;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.Owin.Security.Facebook;
    using Microsoft.Owin.Security.Google;
    using Owin;
    using RepositoryLayer.Context;
    using RepositoryLayer.Interface;
    using RepositoryLayer.Services;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using Operation = Swashbuckle.AspNetCore.Swagger.Operation;

    /// <summary>
    /// Startup page this is the start of the page after the Program.cs file there  have the main method
    /// </summary>
    public class Startup
    {

        ////public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions googleAuthOptions { get; private set; }
        public static FacebookAuthenticationOptions facebookAuthOptions { get; private set; }





        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.Json")
              .AddJsonFile("appsettings.Development.Json", true)
              .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services.
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            //// Add Aplication services
            //// IAccountBusinessLayer is the interface and AccountBusinessLayer is the class
            services.AddTransient<IAccountBusinessLayer, AccountBusinessLayer>();

            ////IAccountRepositoryLayer is the interface and AccountResopsitoryLayer is the class
            services.AddTransient<IAccountRepositoryLayer, AccountRepositoryLayer>();

            //// INoteBusinessLayer is the interface and NoteBusinessLayer is the class 
            services.AddTransient<INotesBusinessLayer, NotesBusinessLayerServices>();

            //// INoteRepositoryLayer is the interface and NoteBusinessLayer is the class 
            services.AddTransient<INotesRepositoryLayer, NotesRepositoryLayer>();

            //// ILabelBusinessLayer is the interface and LabelBusinessLayer is the class 
            services.AddTransient<ILabelBusinessLayer, AdminBusinessService>();

            //// ILabelBusinessLayer is the interface and LabelBusinessLayer is the class 
            services.AddTransient<ILabelRepositoryLayer, LabelRepositoryLayer>();

            //// IAdminBusinessLayer, is the interface and AdminBusinessLayer is the class 
           
            services.AddTransient<IAdminBusinessLayer, AdminBusinessServiceLayer>();

            //// ILabelBusinessLayer is the interface and LabelBusinessLayer is the class 
            services.AddTransient<IAdminRepositoryLayer,AdminRepositoryLayer>();

            //// Add AuthenticationContext service 
            services.AddDbContext<AuthenticationContext>(opts => opts.UseSqlServer(Configuration["ConnectionStrings:IdentityConnection"]));


            //// for the JWT Token
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
            //// Faceboo Authentication code is here
            //services.AddAuthentication(Options => {
            //    Options.DefaultChallengeScheme = FacebookDefaults.AuthenticationScheme;
            //    Options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    Options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //})
            
            ////// Adding the AppId and AppSecret key 
            //.AddFacebook(Options => {
            //   Options.AppId = Configuration["Authentication:Facebook:AppId"];
            //   Options.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //}).AddCookie();

            //// Google Authentication
           // services.AddAuthentication(Options =>
           // {
           //     Options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
           //     Options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
           //     Options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
           // })
           //.AddGoogle(Options =>
           //{
           //    Options.ClientId = Configuration["Authentication:Google:ClientId"];
           //    Options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];

           //});
        }

        /// <summary>
        /// Configures the specified application.
        ///  This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
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
          //  app.UseCors(x => x.AllowAnyOrigin()
            //.AllowAnyHeader().AllowAnyMethod()
            //);
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "My Fundoo API (V 1.0)");
            });
            app.UseCors();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }        
    }
}
