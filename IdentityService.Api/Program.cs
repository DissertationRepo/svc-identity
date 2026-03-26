using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityService.Api.ModelValidators;
using IdentityService.Application.Common;
using IdentityService.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add infrastructure services
builder.Services.AddInfrastructure(builder.Configuration);

// Register FluentValidation validators from this assembly and enable automatic validation
builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterValidator>();
builder.Services.AddFluentValidationAutoValidation();

//Add auto-mapper
builder.Services.AddAutoMapper(
    typeof(IdentityService.Api.Mapping.LoginMappingProfile).Assembly,
    typeof(IdentityService.Infrastructure.Mappings.UserMapping).Assembly,
    typeof(IdentityService.Infrastructure.Mappings.DomainUserMapping).Assembly,
    typeof(IdentityService.Api.Mapping.RegisterMapping).Assembly,
    typeof(IdentityService.Infrastructure.Mappings.RefreshTokenMapping).Assembly
    );

//Add connection string for database context
var conString = builder.Configuration.GetConnectionString("IdentityDB") ??
     throw new InvalidOperationException("Connection string 'IdentityDB'" +
    " not found.");
builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseNpgsql(conString));

// Bind jwt settings to use when configuring JwtBearer
var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtSettings = jwtSection.Get<JwtSettings>() ?? new JwtSettings();
var keyBytes = Encoding.UTF8.GetBytes(jwtSettings.Key);

// Authentication configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettings.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromSeconds(30)
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
