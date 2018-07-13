using swg.Core.Dto;
using swg.Core.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace swg.Core.Stub {
    public class OperationLoggerStub : IOperationLogger, IDisposable {
        private static object _locker = new object();

        private StreamWriter _writer;

        private const int FLUSH_COUNT = 5;

        private int _rowCount = 0;

        private bool disposed = false;

        private static OperationLoggerStub _instance;

        private OperationLoggerStub() {
            var fileName = HttpContext.Current.Server.MapPath("~/Log/operation_log.txt");
            lock (_locker) {
                _writer = File.CreateText(fileName);
            }
        }

        public static OperationLoggerStub GetInstance() {
            if (_instance == null) {
                lock(_locker) {
                    if (_instance == null) {
                        _instance = new OperationLoggerStub();
                    }
                }
            }
            return _instance;
        }

        public Task WriteOperationLogAsync(OperationLogParameter parameters) {
            return Task.Run(() => {
                if (_writer != null) {
                    lock (_locker) {
                        if (_writer != null) {
                            _writer.WriteLine($"{DateTime.Now} {parameters.SessionId} - {parameters.Argument1} {parameters.Operation.OperationName} {parameters.Argument2} = {parameters.OperationResult}");
                            if (++_rowCount == 5) {
                                _writer.Flush();
                                _rowCount = 0;
                            }
                        }
                    }
                }
            });
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposed)
                return;

            if (disposing) {
                if (_writer != null) {
                    _writer.Flush();
                    _writer.Dispose();
                }
            }
            disposed = true;
        }
    }
}