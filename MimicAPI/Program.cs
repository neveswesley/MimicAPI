using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using MimicAPI.Database;
using Microsoft.EntityFrameworkCore;
using MimicAPI.Helpers;
using MimicAPI.V1.Repositories;
using MimicAPI.V1.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MimicContext>(opt =>
{
    opt.UseSqlServer("Server=WESLEY\\SQLEXPRESS;Database=Mimic;User ID=sa;Password=1q2w3e4r5t@#;TrustServerCertificate=True;");
}); 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPalavraRepository, PalavraRepository>();
builder.Services.AddApiVersioning(cfg =>
{
    cfg.ReportApiVersions = true;
    cfg.ApiVersionReader = new HeaderApiVersionReader("api-version");
    cfg.AssumeDefaultVersionWhenUnspecified = true;
    cfg.DefaultApiVersion = new ApiVersion(1,0);
});

#region Auto Mapper Config
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new DTOMapperProfile());
});

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStatusCodePages();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();