namespace swg.Core.Operations {
    public class AddOperation : Operation {

        public AddOperation(string operationName) : base(operationName) { }

        protected override double ExecuteOperation(double arg1, double arg2) {
            return arg1 + arg2;
        }
    }
}