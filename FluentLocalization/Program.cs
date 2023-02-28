using FluentLocalization;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using System.Reflection;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using FluentLocalization.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddControllers()
    .AddFluentValidation(options =>
    {
        options.ImplicitlyValidateChildProperties = true;
        options.ImplicitlyValidateRootCollectionElements = true;

        options.LocalizationEnabled = true;
        //options.ValidatorOptions.LanguageManager.Enabled = true;
        //options.ValidatorOptions.LanguageManager.Culture = CultureInfo.CurrentCulture;

        options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        options.ValidatorOptions.DisplayNameResolver = 

        (type, memberInfo, expression) =>
        {
            if (memberInfo != null && memberInfo is PropertyInfo propertyInfo)
            {
                var displayAttribute = propertyInfo.GetCustomAttribute<DisplayAttribute>();
                if (displayAttribute != null)
                {
                    return displayAttribute.Name;
                }
            }
            return null;
        };
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IStringLocalizerFactory, ResourceManagerStringLocalizerFactory>();

builder.Services.ConfigureLocalization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLocalization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

