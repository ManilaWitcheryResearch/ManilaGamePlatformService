using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manila.GamePlatform.Common
{
    public class Logger : ILogger, IDisposable
    {
        private bool disposed = false;
        private object _messageLock = new object();

        public Logger(string filename)
        {
            // do
        }

        ~Logger()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                // close file?
                disposed = true;
            }

        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Log(string msg)
        {
            // do
            string s = String.Format("[{0}] {1}", String.Format("{0:u}", DateTime.UtcNow), msg);
            lock (_messageLock)
            {
                Console.WriteLine(s);
            }
        }

        public void LogErr(string msg)
        {
            // do
            string s = String.Format("[{0}] {1}", String.Format("{0:u}", DateTime.UtcNow), msg);
            lock (_messageLock)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(s);
                Console.ResetColor();
            }
        }
    }

    public class SmartLogger : ILogger
    {
        private Logger _source;
        private bool _disposed = false;
        private bool _error = false;
        private StringBuilder _builder;

        public SmartLogger(Logger source)
        {
            _source = source;
            _builder = new StringBuilder();
        }

        ~SmartLogger()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // write all logs to log with correct type
                if (_error)
                {
                    _source.LogErr(_builder.ToString());
                }
                else
                {
                    _source.Log(_builder.ToString());
                }
                _disposed = true;
            }

        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Log(string msg)
        {
            _builder.Append(msg + "\n");
        }

        public void LogErr(string msg)
        {
            _error = true;
            Log(msg);
        }
    }
}
