using swg.Attributes.Core.Attributes;
using swg.Core.Creators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace swg.Core.Services {
    public class OperationService {

        private IDictionary<string, IOperationCreator> _allOperations;
        private static object _locker = new object();

        public OperationService() {
            lock(_locker) {
                _allOperations = new Dictionary<string, IOperationCreator>();
            }
        }

        public void Init(IDependencyResolver resolver) {
            var allOperationTypes = this.GetType().Assembly.GetTypes()
                .Where(x => x.GetCustomAttributes(typeof(OperationAttribute), false) != null && x.BaseType  == typeof(OperationCreator))
                .Select(x =>
                        new {
                            OperationName = x.CustomAttributes.First(y => y.AttributeType == typeof(OperationAttribute)).ConstructorArguments.First().Value.ToString(),
                            OperationType = x
                        });
            foreach (var type in allOperationTypes) {
                var creator = resolver.GetService(type.OperationType) as IOperationCreator;
                AddOperation(type.OperationName, creator);
            }
        }

        public void AddOperation(string operationName, IOperationCreator creator) {
            if (String.IsNullOrEmpty(operationName) || creator == null) {
                throw new ArgumentNullException("Operation name or operatoin type is null");
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