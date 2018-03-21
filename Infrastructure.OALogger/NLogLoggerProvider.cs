﻿using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.OALogger
{
    /// <summary>
    /// Provider logger for NLog + Microsoft.Extensions.Logging
    /// </summary>
    public class NLogLoggerProvider : Microsoft.Extensions.Logging.ILoggerProvider
    {
        /// <summary>
        /// NLog options
        /// </summary>
        public NLogProviderOptions Options { get; set; }

        /// <summary>
        /// New provider with default options, see <see cref="Options"/>
        /// </summary>
        public NLogLoggerProvider()
        {
        }

        /// <summary>
        /// New provider with options
        /// </summary>
        /// <param name="options"></param>
        public NLogLoggerProvider(NLogProviderOptions options)
        {
            Options = options;
        }

        /// <summary>
        /// Create a logger with the name <paramref name="name"/>.
        /// </summary>
        /// <param name="name">Name of the logger to be created.</param>
        /// <returns>New Logger</returns>
        public Microsoft.Extensions.Logging.ILogger CreateLogger(string name)
        {
            return new NLogLogger(LogManager.GetLogger(name), Options);
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public void Dispose()
        {
        }
    }
}
