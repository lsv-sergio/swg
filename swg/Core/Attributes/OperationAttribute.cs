using System;

namespace swg.Attributes.Core.Attributes {

    [System.AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class OperationAttribute : Attribute {
        private readonly string _operationName;

        public OperationAttribute(string operationName) {
            this._operationName = operationName;
        }

        public string OperationName {
            get { return _operationName; }
        }
    }
}