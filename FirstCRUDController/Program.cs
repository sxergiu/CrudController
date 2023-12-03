using FirstCRUDController.Entities.Maids;
using FirstCRUDController.Entities.Rooms;
using FirstCRUDController.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseNpgsql(configuration.GetConnectionString("ConnectionString")));

builder.Services.AddScoped<IRoomRepo, RoomRepo>();
builder.Services.AddScoped<IMaidRepository, MaidRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

