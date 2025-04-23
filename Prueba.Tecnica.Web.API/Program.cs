using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using Prueba.Tecnica.Web.API.Middlewares.Behaviors;
using Prueba.Tecnica.Web.API.Middlewares.ErrosHandling;
using Prueba.Tecnica.Web.Application;
using Prueba.Tecnica.Web.Application.Validatos;
using Prueba.Tecnica.Web.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
//builder.Services.AddControllers();

builder.Services.AddControllers()
    .AddFluentValidation(static fv => fv.RegisterValidatorsFromAssemblyContaining<SaveFileCommandValidator>());

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 5MB  
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
