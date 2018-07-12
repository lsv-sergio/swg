namespace swg.Core.Operations {
    public abstract class Operation : IOperation {

        public Operation(string operationName) {
            OperationName = operationName;
        }

        public string OperationName { get; protected set; }

        protected abstract double ExecuteOperation(double arg1, double arg2);

        public virtual double Execute(double arg1, double arg2) {
            var result = ExecuteOperation(arg1, arg2);
            return result;
        }
    }
}