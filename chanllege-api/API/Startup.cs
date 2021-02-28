using System.Globalization;
using API.Extensions;
//using API.Helpers;
using API.Middleware;
//using API.Workers;
//using Application.Users;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;


namespace API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
      
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
            );
            
            services.AddApplicationServices();
            
            services.AddIdentityServices(_configuration);
            
            services.AddSwaggerDocumentation();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });

            //services.AddMediatR(typeof(CreateUser.CreateUserCommand).Assembly);

            services.AddControllers();
                // .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<CreateUser>())
                // .AddNewtonsoftJson(options =>
                //     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            // services.AddAutoMapper(typeof(MappingProfiles));
            //
            // services.AddHostedService<RevisionEmailWorker>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            //app.UseAuthentication();

            //app.UseAuthorization();

            app.UseSwaggerDocumentation();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}