using swg.Core.Dto;
using System;
using System.Threading.Tasks;

namespace swg.Core.Services {
    public interface IOperationLogger {
        Task WriteOperationLogAsync(OperationLogParameter parameters);
    }
}
