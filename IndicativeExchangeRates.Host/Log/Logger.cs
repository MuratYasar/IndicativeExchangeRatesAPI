using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IndicativeExchangeRates.Host.Log
{
    internal class Logger
    {
        private static Logger _logger;
        private ILog _log;

        public static Logger Instance
        {
            get
            {
                if (_logger == null)
                {
                    _logger = new Logger();
                }
                return _logger;
            }
        }

        private Logger()
        {
        }

        private void Initialize()
        {
            _log = LogManager.GetLogger(typeof(Logger));

            log4net.Util.LogLog.InternalDebugging = true;

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo((new System.IO.FileInfo(Assembly.GetExecutingAssembly().Location)).DirectoryName + @"\Log\log4net.config"));
        }

        public void Fatal(Exception ex, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (_log == null)
            {
                Initialize();
            }

            _log.Fatal(string.Format("Fatal Logged From method {0} in file {1} at line {2}", memberName, sourceFilePath, sourceLineNumber), ex);
        }

        public void Error(Exception ex, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (_log == null)
            {
                Initialize();
            }

            _log.Error(string.Format("Error Logged From method {0} in file {1} at line {2}", memberName, sourceFilePath, sourceLineNumber), ex);
        }

        public void Warning(Exception ex, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (_log == null)
            {
                Initialize();
            }

            _log.Warn(string.Format("Warning from method {0} in file {1} at line {2}", memberName, sourceFilePath, sourceLineNumber), ex);
        }

        public void Debug(Exception ex, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (_log == null)
            {
                Initialize();
            }

            _log.Debug(string.Format("Debug entry from method {0} in file {1} at line {2}", memberName, sourceFilePath, sourceLineNumber), ex);
        }

        public void Info(object message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (_log == null)
            {
                Initialize();
            }

            _log.Info(string.Format("Info entry from method {0} in file {1} at line {2} with message [{3}]", memberName, sourceFilePath, sourceLineNumber, message));
        }
    }
}
