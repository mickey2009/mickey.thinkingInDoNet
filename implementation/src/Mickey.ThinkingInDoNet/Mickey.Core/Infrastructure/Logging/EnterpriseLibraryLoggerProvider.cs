using Mickey.Core.ComponentModel;
using Microsoft.Framework.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;

namespace Mickey.Core.Infrastructure.Logging
{
    public class EnterpriseLibraryLoggerProvider : ILoggerProvider
    {
        LoggingConfiguration m_Configuration;

        private List<IDisposable> m_DisposableObjects;

        public EnterpriseLibraryLoggerProvider()
        {
            m_DisposableObjects = new List<IDisposable>();
        }

        public EnterpriseLibraryLoggerProvider(LoggingConfiguration config) : this()
        {
            Requires.NotNull(config, "config");
            m_Configuration = config;
        }

        public ILogger CreateLogger(string name)
        {
            var item = new EnterpriseLibraryLogger(m_Configuration ?? LoggingConfigurationFactory.Create(name));
            this.m_DisposableObjects.Add(item);
            return item;
        }

        public void Dispose()
        {
            foreach (var disposableObject in m_DisposableObjects)
            {
                disposableObject.Dispose();
            }
        }
    }
}
