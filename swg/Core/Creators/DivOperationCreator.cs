using swg.Attributes.Core.Attributes;
using swg.Core.Operations;
using swg.Core.Services;

namespace swg.Core.Creators {
    [Operation("/")]
    public class DivOperationCreator : OperationCreator {

        protected override IOperation GetOperation(string operationName) {
            return new DivOperation(operationName);
        }
    }
}