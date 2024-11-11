using Application.Commons;
using Infrastructure;
using Infrastructure.Mappers;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Application.IService;
using Application.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PRN231_PetCare;
using CloudinaryDotNet;
using Infrastructure.ViewModels.Cloud;
using System.Net;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

// Add configuration from appsettings.Development.json
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();



// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddScoped<Cloudinary>(sp =>
{
    var cloudinaryConfig = builder.Configuration.GetSection("Cloudinary").Get<CloudinarySettings>();
    var cloudinaryAccount = new Account(
        cloudinaryConfig.CloudName,
        cloudinaryConfig.ApiKey,
        cloudinaryConfig.ApiSecret
    );
    return new Cloudinary(cloudinaryAccount);
});


builder.Services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext using the connection string from appsettings.json
builder.Services.AddDbContext<PetCareDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Local")));

// Bind AppConfiguration and register it
builder.Services.Configure<AppConfiguration>(builder.Configuration);

// Add custom services
builder.Services.AddInfrastructuresService();
    builder.Services.AddWebAPIService();
builder.Services.AddAutoMapper(typeof(MapperConfigurationsProfile));
builder.Services.AddMemoryCache();

// Register IAuthenticationService
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

// Add authentication with JWT Bearer tokens
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtConfig = builder.Configuration.GetSection("JWTSection").Get<JWTSection>();

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtConfig.Issuer,
            ValidAudience = jwtConfig.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Staff", policy => policy.RequireRole("Staff"));
    options.AddPolicy("Customer", policy => policy.RequireRole("Customer"));
});

// Configure Swagger for JWT authentication and XML documentation
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "PetCare.API",
    });

    // Add JWT authentication support in Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.\n\nEnter your token in the text input below.\n\nExample: '12345abcde'",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    // Configure JWT security requirement in Swagger
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
            Array.Empty<string>()
        }
    });
});

// Configure the SMTP client settings
builder.Services.AddSingleton<SmtpClient>(provider =>
{
    var smtpClient = new SmtpClient
    {
        Host = builder.Configuration["EmailSettings:SmtpHost"], // e.g., "smtp.gmail.com"
        Port = int.Parse(builder.Configuration["EmailSettings:SmtpPort"]), // e.g., 587
        Credentials = new NetworkCredential(
            builder.Configuration["EmailSettings:Username"], // Your email address
            builder.Configuration["EmailSettings:Password"]  // Your email password or app-specific password
        ),
        EnableSsl = bool.Parse(builder.Configuration["EmailSettings:EnableSsl"]) // true or false depending on the provider
    };

    return smtpClient;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting(); // Ensure routing is enabled

// Enable CORS
app.UseCors("AllowAll"); // Use the CORS policy defined earlier

app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization();

app.MapControllers();

app.Run();
