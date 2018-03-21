using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.OALogger;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace OALogger
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //var services = new ServiceCollection();

            //services.AddSingleton<ILoggerFactory, LoggerFactory>();
            //services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            //services.AddLogging((builder) => builder.SetMinimumLevel(LogLevel.Trace));

            //var serviceProvider = services.BuildServiceProvider();

            //var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            ////configure NLog
            //loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
            //loggerFactory.ConfigureNLog("nlog.config");

            //var logger = NLog.LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();

            //try
            //{
            //    logger.Debug("init main");
            //    BuildWebHost(args).Run();
            //}
            //catch (Exception ex)
            //{
            //    //NLog: catch setup errors
            //    logger.Error(ex, "Stopped program because of exception");
            //    throw;
            //}

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
