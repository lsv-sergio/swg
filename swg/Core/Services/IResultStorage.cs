using System;
using System.Threading.Tasks;

namespace swg.Core.Services {
    public interface IResultStorage {
        Task<Guid> SaveResultToStorage(double result);
        Task<double?> GetResultByKeyAsync(Guid key);
    }
}
