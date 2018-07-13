using swg.Core.Creators;
using System.Collections.Generic;

namespace swg.Core.Services {
    public interface IOperationStorage {

        void AddOperation(string operationName, IOperationCreator creator);

        IEnumerable<string> GetAllOperationNames();

        IOperationCreator GetCreatorByOperationName(string operationName);
    }
}
