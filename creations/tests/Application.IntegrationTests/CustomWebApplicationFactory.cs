﻿using Creations.Application.Common.Interfaces;
using Creations.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

using static Creations.Application.IntegrationTests.Testing;

namespace Creations.Application.IntegrationTests;
internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddConfiguration(integrationConfig);
        });

        //builder.ConfigureServices((builder, services) =>
        //{
        //    services
        //        .Remove<DbContextOptions<ApplicationDbContext>>()
        //        .AddDbContext<ApplicationDbContext>((sp, options) =>
        //            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        //                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        //});
    }
}
