using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ServiceApp.BLL.Interfaces;
using ServiceApp.BLL.Services;
using ServiceApp.DAL.EFContext;
using ServiceApp.DAL.Repository;


namespace ServiceApp.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment configuration)
        {
            this.Configuration = new ConfigurationBuilder()
                .SetBasePath(configuration.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{configuration.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("FirstConnectionString");
            services.AddDbContext<ServiceContext>(options => options.UseSqlServer(connection));


            services.AddCors(options => options.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));
            services.AddMvc();

            // var appSettingsSection = Configuration.GetSection("ApplicationSettings");
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });

            // configure jwt authentication
            // var appSettings = appSettingsSection.Get<AppSettings>();
            var key = System.Text.Encoding.ASCII.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerOption =>
            {
                JwtBearerOption.RequireHttpsMetadata = false;
                JwtBearerOption.SaveToken = true;
                JwtBearerOption.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IProductService, ProductService>();
        }


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
            app.UseCors("AllowAllOrigins");


            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();

        }
    }
}
