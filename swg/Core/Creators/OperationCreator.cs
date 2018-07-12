using swg.Attributes.Core.Attributes;
using swg.Core.Operations;
using System.Linq;
using System;
using swg.Core.Services;

namespace swg.Core.Creators {
    public abstract class OperationCreator: IOperationCreator {

        protected string GetOperationName() {
            var attribute = this.GetType().GetCustomAttributes(typeof(OperationAttribute), false).First() as OperationAttribute;
            if (attribute == null) {
                throw new ArgumentNullException("Operation name");
            }
            return attribute.OperationName;
        }

        protected abstract IOperation GetOperation(string operationName);

        public virtual IOperation CreateOperation() {
            var operationName = GetOperationName();
            return GetOperation(operationName);
        }
    }
}