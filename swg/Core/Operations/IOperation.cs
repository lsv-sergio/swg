namespace swg.Core.Operations {

    public interface IOperation {

        string OperationName { get; }

        double Execute(double arg1, double arg2);
    }
}
