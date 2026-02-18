using Microsoft.EntityFrameworkCore;
using Learning_ASP_Dot_Net.Data;
using Learning_ASP_Dot_Net.Behaviours.Interfaces;
using Learning_ASP_Dot_Net.Behaviours;
using Learning_ASP_Dot_Net.DataAccess.Interfaces;
using Learning_ASP_Dot_Net.DataAccess;
using Learning_ASP_Dot_Net.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Learning_ASP_Dot_Net.Services;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "Jwt",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Enter Your Jwt Access Token",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    options.AddSecurityDefinition("Bearer", jwtSecurityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>()}
    });
});

//Adding DI
builder.Services.AddScoped<IStudentControllBehaviour, StudentControllBehaviour>();
builder.Services.AddScoped<IStudentControllDataAccess, StudentControllDataAccess>();
builder.Services.AddScoped<JwtServices>();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).
AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"])),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true
    };
});

builder.Services.AddAuthorization();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.Use(async (ctx, next) => { 
    Console.WriteLine("Use Middleware Executed");
    await next(ctx);
    Console.WriteLine("Use Middleware Executed After Request");
});

app.UseMiddleware<CustomMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();
