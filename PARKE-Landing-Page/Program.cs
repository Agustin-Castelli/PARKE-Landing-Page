using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PARKE_Landing_Page.Data.Interfaces;
using PARKE_Landing_Page.Data.Repositories;
using PARKE_Landing_Page.Data.Services;
using PARKE_Landing_Page.Repositories;
using PARKE_Landing_Page.Services;
using PARKE_Landing_Page.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Repositories
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<INewRepository, NewRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IMachineRepository, MachineRepository>();

#endregion

#region Services
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAuthenticationServiceAdmin, AuthenticationServiceAdmin>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IMachineService, MachineService>();
builder.Services.AddScoped<IAuthenticationServiceClient, AuthenticationServiceClient>();
builder.Services.AddScoped<IEmailService, EmailService>();

#endregion

#region Authentication
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("ApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Introduzca el token JWT como: Bearer {token}"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiBearerAuth"
                }
            },
            new List<string>()
        }
    });
});

builder.Services.Configure<AuthenticationServiceAdmin.AutenticacionServiceOptions>(
    builder.Configuration.GetSection("Authentication"));
builder.Services.Configure<AuthenticationServiceClient.AutenticacionServiceOptions>(
    builder.Configuration.GetSection("Authentication"));


builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
            
        };
    });

builder.Services.AddAuthorization(options =>
{
    // Política para clientes y administradores
    options.AddPolicy("Client", policy =>
        policy.RequireRole("Client", "Admin"));

    // Política solo para administradores
    options.AddPolicy("Admin", policy =>
        policy.RequireRole("Admin"));
});



#endregion

#region Database  
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 2))));
#endregion

#region CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:5174", "http://localhost:5173", "https://localhost:7062")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
#endregion

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
