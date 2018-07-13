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

        private void Init(IComponentContext resolver) {
            var allOperationTypes = this.GetType().Assembly.GetTypes()
                .Where(x => x.GetCustomAttributes(typeof(OperationAttribute), false) != null && x.BaseType  == typeof(OperationCreator))
                .Select(x =>
                        new {
                            OperationName = x.CustomAttributes.First(y => y.AttributeType == typeof(OperationAttribute)).ConstructorArguments.First().Value.ToString(),
                            OperationType = x
                        });
            foreach (var type in allOperationTypes) {
                var creator = resolver.Resolve(type.OperationType) as IOperationCreator;
                AddOperation(type.OperationName, creator);
            }
        }

        public static IOperationStorage GetInstance(IComponentContext resolver) {
            if (_instance != null) {
                return _instance;
            }
            lock (_locker) {
                if (_instance == null) {
                    _instance = new OperationStorage();
                    _instance.Init(resolver);
                }
            }
            return _instance;
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