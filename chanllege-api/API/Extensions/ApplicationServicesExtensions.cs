using System.Linq;
using Application.Helpers;
using Application.Interfaces;
using Application.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRandomStringGenerator, RamdomStringGeneratorHelper>();
            services.AddScoped<ICustomMapper, CustomMapper>();
            services.AddScoped<IUploadFiles, UploadFilesHelper>();
            services.AddScoped<IDocumentsUrl, DocumentsUrlHelper>();
            // services.AddScoped<IEmailSender, EmailService>();
            // services.AddScoped<IUserAccessor, UserAccessor>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };
                    
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;

        }
    }
}