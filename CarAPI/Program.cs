using CarAPI.Database;
using CarAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CarContext>(options => options.UseInMemoryDatabase("DefaultConnection"));
//builder.Services.AddTransient<DatabaseSeeder>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed Data
//var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
//using (var scope = scopedFactory.CreateScope())
//{
//    var service = scope.ServiceProvider.GetService<DatabaseSeeder>();
//    service.Seed();
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

