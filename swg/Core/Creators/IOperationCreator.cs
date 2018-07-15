using swg.Core.Operations;

namespace swg.Core.Creators {
    public interface IOperationCreator {

        IOperation CreateOperation();

        string GetOperationName();

    }
}
