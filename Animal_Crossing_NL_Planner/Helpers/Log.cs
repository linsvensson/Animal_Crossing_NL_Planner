using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.IO;

namespace Animal_Xing_Planner
{
    public class Log : ILogger
    {
        private Logger _logger;

        public Log()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Init(string message)
        {
            _logger.Trace(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception x)
        {
            _logger.Error(this.BuildExceptionMessage(x));
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(Exception x)
        {
            _logger.Fatal(this.BuildExceptionMessage(x));
        }

        public void ConfigureLogger()
        {
            // Step 1. Create configuration object 
            LoggingConfiguration config = new LoggingConfiguration();

            // Step 2. Create targets and add them to the configuration 
            FileTarget fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            string dir = @"log";

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            fileTarget.FileName = dir + "/log.txt";
            fileTarget.Layout = @"${date}|${level:uppercase=true}|${message}";

            LoggingRule rule1 = new LoggingRule("*", LogLevel.Info, fileTarget);
            config.LoggingRules.Add(rule1);

            // Step 5. Activate the configuration
            LogManager.Configuration = config;
        }
    }

    public static class LoggerExt
    {
        public static string BuildExceptionMessage(this ILogger logger, Exception x)
        {
            Exception logException = x;

            if (x.InnerException != null)
                logException = x.InnerException;

            string strErrorMsg = Environment.NewLine + "Message :" + logException.Message;
            strErrorMsg += Environment.NewLine + "Source :" + logException.Source;
            strErrorMsg += Environment.NewLine + "Stack Trace :" + logException.StackTrace;
            strErrorMsg += Environment.NewLine + "TargetSite :" + logException.TargetSite;

            return strErrorMsg;
        }
    }

    public interface ILogger
    {
        void Info(string message);
        void Warn(string message);
        //void Debug(string message);
        void Error(string message);
        void Error(Exception x);
        void Fatal(string message);
        void Fatal(Exception x);
    }
}
