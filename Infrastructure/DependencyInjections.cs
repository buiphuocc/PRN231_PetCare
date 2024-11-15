﻿
using Application;
using Application.Commons;
using Infrastructure;
using Infrastructure.IRepositories;
using Infrastructure.IService;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services)
        {
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICurrentTime, CurrentTime>();
            services.AddScoped<AppConfiguration>();




            services.AddScoped<ICatRepo, CatRepo>();
            services.AddScoped<ICatProfileRepo, CatProfileRepo>();
            services.AddScoped<IAdoptionContractRepo, AdoptionContractRepo>();
            services.AddScoped<IShelterRepo, ShelterRepo>();
            services.AddScoped<IDonationRepo, DonationRepo>();
            services.AddScoped<IImageRepo, ImageRepo>();




            //services.AddDbContext<AppDbContext>(option => option.UseSqlServer(databaseConnection));
            return services;
        }
    }
}
