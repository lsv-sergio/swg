using swg.Attributes.Core.Attributes;
using swg.Core.Operations;
using swg.Core.Services;

namespace swg.Core.Creators {
    [Operation("*")]
    public class MultOperationCreator : OperationCreator {

        protected override IOperation GetOperation(string operationName) {
            return new MultOperation(operationName);
        }
    }
}