using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using log4net;
using log4net.Config;
using Newtonsoft.Json;

namespace Detention_facility
{
    public static class CustomLogging
    {
        private static ILog _log = null;
        private static string _logFile = null;

        public static void Initialize(string ApplicationPath)
        {
            _logFile = Path.Combine(ApplicationPath, "Logs", "Info.log");
            GlobalContext.Properties["LogFileName"] = _logFile;

            log4net.Config.XmlConfigurator.Configure(new FileInfo(Path.Combine(ApplicationPath, "Log4Net.config")));

            _log = LogManager.GetLogger("Log");
        }

        public static string LogFile
        {
            get { return _logFile; }
        }

        public enum TracingLevel
        {
            ALL, DEBUG, INFO, WARN, ERROR, FATAL, OFF
        }              

        public static void LogMessage(TracingLevel Level, string Message)
        {
            switch (Level)
            {
                case TracingLevel.DEBUG:
                    _log.Debug(Message);
                    break;

                case TracingLevel.INFO:
                    _log.Info(Message);
                    break;

                case TracingLevel.WARN:
                    _log.Warn(Message);
                    break;

                case TracingLevel.ERROR:
                    _log.Error(Message);
                    break;

                case TracingLevel.FATAL:
                    _log.Fatal(Message);
                    break;
            }
        }

        internal static string ModelStatusConverter(ModelStateDictionary modelState)
        {
         string errors = JsonConvert.SerializeObject(modelState.Values
        .SelectMany(state => state.Errors)
        .Select(error => error.ErrorMessage));
            return errors;
        }
    }
}