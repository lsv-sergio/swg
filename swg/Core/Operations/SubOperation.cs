namespace swg.Core.Operations {
    public class SubOperation : Operation {

        public SubOperation(string operationName) : base(operationName) { }

        protected override double ExecuteOperation(double arg1, double arg2) {
            return arg1 - arg2;
        }
    }
}