using Microsoft.EntityFrameworkCore;
using Learning_ASP_Dot_Net.Data;
using Learning_ASP_Dot_Net.Behaviours.Interfaces;
using Learning_ASP_Dot_Net.Behaviours;
using Learning_ASP_Dot_Net.DataAccess.Interfaces;
using Learning_ASP_Dot_Net.DataAccess;
using Learning_ASP_Dot_Net.Middlewares;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adding DI
builder.Services.AddScoped<IStudentControllBehaviour, StudentControllBehaviour>();
builder.Services.AddScoped<IStudentControllDataAccess, StudentControllDataAccess>();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.MapControllers();

app.Run();
