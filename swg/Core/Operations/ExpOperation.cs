using System;

namespace swg.Core.Operations {
    public class ExpOperation : Operation {

        public ExpOperation(string operationName) : base(operationName) { }

        protected override double ExecuteOperation(double arg1, double arg2) {
            return Math.Pow(arg1, arg2);
        }
    }
}