using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace swg.Core.Services.Stub {
    public class ResultStorageStub : IResultStorage {

        private static object _resultLocker = new object();

        private IDictionary<Guid, double> _storage;

        public ResultStorageStub() {
            lock(_resultLocker) {
                _storage = new Dictionary<Guid, double>();
            }
        }

        public Task<double?> GetResultByKeyAsync(Guid key) {
            return Task.Run(() => {
                double? result = null;
                if (_storage.Keys.Contains(key)) {
                    lock (_resultLocker) {
                        if (_storage.Keys.Contains(key)) {
                            result = _storage[key];
                        }
                    }
                }
                return result;
            });
        }

        public Task<Guid> SaveResultToStorage(double result) {
            return Task.Run(() => {
                var key = Guid.NewGuid();
                    lock (_resultLocker) {
                        _storage[key] = result;
                    }
                return key;
            });
        }
    }
}

