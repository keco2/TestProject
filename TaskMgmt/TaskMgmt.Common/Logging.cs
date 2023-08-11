using NLog;

namespace TaskMgmt.Common
{
    public class Logging
    {
        /// <summary>
        /// Source: Official NLog Github site
        /// https://github.com/NLog/NLog/wiki/Configure-from-code
        /// Find more Output-Layout options, fields, transformations:
        /// https://nlog-project.org/config/?tab=layout-renderers
        /// </summary>
        public static void LoggingSetUp()
        {
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "${basedir}/logfile.txt" };
            logfile.Layout = "${longdate}" +
                "|${level:truncate=1:uppercase=true}" +
                "|T${threadid:padCharacter=0:padding=2:fixedLength=True}" +
                "|${callsite:className=True:includeNamespace=True:fileName=False:includeSourcePath=False:methodName=True}" +
                "|${message}";
            logfile.ArchiveOldFileOnStartupAboveSize = 5000000;
            logfile.ArchiveNumbering = NLog.Targets.ArchiveNumberingMode.DateAndSequence;
            var minLogLevel = LogLevel.Info;
#if DEBUG
            minLogLevel = LogLevel.Debug;
#endif

            // Rules for mapping loggers to targets
            config.AddRule(minLogLevel, LogLevel.Fatal, logfile);

            // Log levels:
            // Trace - very detailed logs, which may include high - volume information such as protocol payloads.This log level is typically only enabled during development
            // Debug - debugging information, less detailed than trace, typically not enabled in production environment.
            // Info - information messages, which are normally enabled in production environment
            // Warn - warning messages, typically for non - critical issues, which can be recovered or which are temporary failures
            // Error - error messages - most of the time these are Exceptions
            // Fatal - very serious errors!

            // Apply config
            NLog.LogManager.Configuration = config;
        }
    }
}
