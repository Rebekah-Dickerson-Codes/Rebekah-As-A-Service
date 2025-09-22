using Microsoft.AspNetCore.Authentication;
using Rebekah_As_A_Service.DataAccess;
using Rebekah_As_A_Service.DataAccess.Interfaces;
using Rebekah_As_A_Service.Processors;
using Rebekah_As_A_Service.Processors.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication("Basic").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null); ;
builder.Services.AddControllers();
//dependency injection
builder.Services.AddSingleton<IAdminProcessor, AdminProcessor>();
builder.Services.AddSingleton<IFactsProcessor, FactsProcessor>();
builder.Services.AddSingleton<ICategoryDBAccessor, CategoryDBAccessor>();
builder.Services.AddSingleton<IFactDBAccessor, FactDBAccessor>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
