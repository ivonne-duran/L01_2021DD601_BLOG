using Microsoft.EntityFrameworkCore;
using L01_2021DD601.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddDbContext<UsuariosContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("blogDbconnection")));

builder.Services.AddDbContext<PublicacionesContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("blogDbconnection")));

builder.Services.AddDbContext<ComentariosContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("blogDbconnection")));

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

app.UseAuthorization();

app.MapControllers();

app.Run();
