﻿using System;
using System.Reflection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Common;
using NLog.Config;

namespace Infrastructure.OALogger
{
    /// <summary>
    /// Helpers for .NET Core
    /// </summary>
    public static class ConfigureExtensions
    {
        /// <summary>
        /// Enable NLog as logging provider in .NET Core.
        /// </summary>
        /// <param name="factory"></param>
        /// <returns>ILoggerFactory for chaining</returns>
        public static ILoggerFactory AddLogger(this ILoggerFactory factory)
        {
            return AddLogger(factory, null);
        }

        /// <summary>
        /// Enable NLog as logging provider in .NET Core.
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="options">NLog options</param>
        /// <returns>ILoggerFactory for chaining</returns>
        public static ILoggerFactory AddLogger(this ILoggerFactory factory, NLogProviderOptions options)
        {
            ConfigureHiddenAssemblies();
            using (var provider = new NLogLoggerProvider(options))
            {
                factory.AddProvider(provider);
            }
            return factory;
        }

//#if !NETCORE1_0
//        /// <summary>
//        /// Enable NLog as logging provider in .NET Core.
//        /// </summary>
//        /// <param name="factory"></param>
//        /// <returns>ILoggerFactory for chaining</returns>
//        public static ILoggingBuilder AddNLog(this ILoggingBuilder factory)
//        {
//            return AddNLog(factory, null);
//        }

//        /// <summary>
//        /// Enable NLog as logging provider in .NET Core.
//        /// </summary>
//        /// <param name="factory"></param>
//        /// <param name="options">NLog options</param>
//        /// <returns>ILoggerFactory for chaining</returns>
//        public static ILoggingBuilder AddNLog(this ILoggingBuilder factory, NLogProviderOptions options)
//        {
//            using (var provider = new NLogLoggerProvider(options))
//            {
//                factory.AddProvider(provider);
//            }
//            return factory;
//        }
//#endif

        /// <summary>
        /// Ignore assemblies for ${callsite}
        /// </summary>
        private static void ConfigureHiddenAssemblies()
        {
//#if NETCORE1_0 && !NET451
            InternalLogger.Trace("Hide assemblies for callsite");

            SafeAddHiddenAssembly("Microsoft.Logging");

            SafeAddHiddenAssembly("Microsoft.Extensions.Logging");
            SafeAddHiddenAssembly("Microsoft.Extensions.Logging.Abstractions");
            SafeAddHiddenAssembly("NLog.Extensions.Logging");

            //try the Filter ext, this one is not mandatory so could fail
            SafeAddHiddenAssembly("Microsoft.Extensions.Logging.Filter", false);
//#endif
        }

//#if NETCORE1_0 && !NET451
        private static void SafeAddHiddenAssembly(string assemblyName, bool logOnException = true)
        {
            try
            {
                InternalLogger.Trace("Hide {0}", assemblyName);
                var assembly = Assembly.Load(new AssemblyName(assemblyName));
                LogManager.AddHiddenAssembly(assembly);
            }
            catch (Exception ex)
            {
                if (logOnException)
                {
                    InternalLogger.Debug(ex, "Hiding assembly {0} failed. This could influence the ${callsite}", assemblyName);
                }
            }
        }
//#endif

        /// <summary>
        /// Apply NLog configuration from XML config.
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="configFileRelativePath">relative path to NLog configuration file.</param>
        /// <returns>Current configuration for chaining.</returns>
        public static LoggingConfiguration ConfigureLogger(this ILoggerFactory loggerFactory, string configFileRelativePath)
        {
            ConfigureHiddenAssemblies();
            return LogManager.LoadConfiguration(configFileRelativePath).Configuration;
        }

        /// <summary>
        /// Apply NLog configuration from config object.
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="config">New NLog config.</param>
        /// <returns>Current configuration for chaining.</returns>
        public static LoggingConfiguration ConfigureLogger(this ILoggerFactory loggerFactory, LoggingConfiguration config)
        {
            ConfigureHiddenAssemblies();
            LogManager.Configuration = config;
            return config;
        }
    }
}
