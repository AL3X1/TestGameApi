using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RedRiftTestTask.Common.Builders;
using RedRiftTestTask.Common.Interfaces;
using RedRiftTestTask.Common.Repositories;
using RedRiftTestTask.Domain.Commands.Results.Lobbies;
using RedRiftTestTask.Domain.Services;
using RedRiftTestTask.Infrastructure.Contexts;
using RedRiftTestTask.Infrastructure.Factories;

namespace RedRiftTestApplication.Initializers
{
    public class AutofacInitializer
    {
        public static IServiceProvider Initialize(IServiceCollection services)
        {
            services.AddAutofac();

            ContainerBuilder containerBuilder = new ContainerBuilder();
            List<Assembly> assemblies = GetSolutionAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                containerBuilder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(ICommand<>));
                containerBuilder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IQuery<,>));
            }

            containerBuilder.RegisterType<CommandBuilder>().As<ICommandBuilder>().SingleInstance();
            containerBuilder.RegisterType<QueryBuilder>().As<IQueryBuilder>().SingleInstance(); 
            containerBuilder.RegisterType<RuntimeRepository>().As<IRuntimeRepository>();

            containerBuilder.RegisterType<ApplicationDbContext>().As<DbContext>()
                .WithParameter("options", DbContextOptionsFactory.Get<ApplicationDbContext>())
                .InstancePerDependency();

            containerBuilder.RegisterType<StartGameService>().As<IHostedService>();
            containerBuilder.RegisterType<GamingService>().As<IHostedService>();
            
//            LoggerFactory loggerFactory = new LoggerFactory();

//            loggerFactory.AddDebug();
//            loggerFactory.AddConsole();

//            containerBuilder.RegisterInstance(loggerFactory).As<ILoggerFactory>();
//            containerBuilder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();
            
            containerBuilder.Populate(services);
            
            IContainer container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }

        private static List<Assembly> GetSolutionAssemblies()
        {
            List<Assembly> assemblies = new List<Assembly>
            {
                // Application
                typeof(Startup).Assembly,
                // Common
                typeof(CommandBuilder).Assembly,
                // Domain
                typeof(CreationResult).Assembly
            };

            return assemblies;
        }
    }
}