using Application;

using Infrastructure;
using Infrastructure.IRepositories;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication;
using System.Diagnostics;

namespace PRN231_PetCare
{
    public static class DependencyInject
    {
        public static IServiceCollection AddWebAPIService(this IServiceCollection services)
        {
           // services.AddControllers().AddJsonOptions (option=> option.JsonSerializerOptions.PropertyNamingPolicy=System.Text.Json.JsonNamingPolicy.KebabCaseLower);
            /*services.AddFluentValidation();*/ 
            
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHealthChecks();
            //services.AddCors();
            /*services.AddSingleton<GlobalExceptionMiddleware>();
            services.AddSingleton<PerformanceMiddleware>();*/
            services.AddSingleton<Stopwatch>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            //services.AddSingleton<ICurrentTime, CurrentTime>();
            //services.AddScoped<IClaimsService, ClaimsService>();

          
            services.AddHttpContextAccessor();


          

  
            //services.AddScoped, ProductService>();







            services.AddScoped<IUserRepo, UserRepo>();

            return services;
        }
    }
}
