using Autofac;
using swg.Attributes.Core.Attributes;
using swg.Core.Creators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace swg.Core.Services {
    public class OperationStorage: IOperationStorage {

        private IDictionary<string, IOperationCreator> _allOperations;
        private static object _locker = new object();
        private static OperationStorage _instance;

        private OperationStorage() {
           _allOperations = new Dictionary<string, IOperationCreator>();
        }

        public static IOperationStorage GetInstance() {
            if (_instance != null) {
                return _instance;
            }
            lock (_locker) {
                if (_instance == null) {
                    _instance = new OperationStorage();
                }
            }
            return _instance;
        }

        public void AddOperationCreators(IEnumerable<IOperationCreator> creators) {
            if (creators == null) {
                throw new ArgumentNullException("array of creators");
            }
            foreach (var creator in creators) {
               
                AddOperationCreator(creator);
            }
        }

        public void AddOperationCreator(IOperationCreator creator) {
            var operationName = creator?.GetOperationName();
            if (String.IsNullOrEmpty(operationName)) {
                throw new ArgumentNullException("Operation name or operatin's creator is null");
            }
            lock (_locker) {
                _allOperations[operationName] = creator;
            }
        }

        public IEnumerable<string> GetAllOperationNames() {
            IEnumerable<string> result;
            lock (_locker) {
                result = _allOperations.Keys;
            }
            return result;
        }

        public IOperationCreator GetCreatorByOperationName(string operationName) {
            if (_allOperations.Keys.Contains(operationName)) {
                lock (_locker) {
                    if (_allOperations.Keys.Contains(operationName)) {
                        return _allOperations[operationName];
                    }
                }
            }
            return null;
        }
    }
}