using swg.Core.Operations;

namespace swg.Core.Dto {
    public class OperationLogParameter {
        public IOperation Operation { get; private set; }
        public double Argument1 { get; private set; }
        public double Argument2 { get; private set; }
        public double OperationResult { get; private set; }

        private OperationLogParameter(IOperation operation, double arg1, double arg2, double operationResult) {
            Operation = operation;
            Argument1 = arg1;
            Argument2 = arg2;
            OperationResult = operationResult;
        }

        public static OperationLogParameter Create(IOperation operation, double arg1, double arg2, double operationResult) {
            return new OperationLogParameter(operation, arg1, arg2, operationResult);
        }
    }
}