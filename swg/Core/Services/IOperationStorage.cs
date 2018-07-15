using swg.Core.Creators;
using System;
using System.Collections.Generic;

namespace swg.Core.Services {
    public interface IOperationStorage {

        void AddOperationCreator(IOperationCreator creator);

        void AddOperationCreators(IEnumerable<IOperationCreator> operations);

        IEnumerable<string> GetAllOperationNames();

        IOperationCreator GetCreatorByOperationName(string operationName);
    }
}
