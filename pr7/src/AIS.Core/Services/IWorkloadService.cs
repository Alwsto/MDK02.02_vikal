using AIS.Core.Models;

namespace AIS.Core.Services;

public interface IWorkloadService
{
    Task<IReadOnlyList<WorkloadItem>> GetWorkloadsAsync(CancellationToken cancellationToken = default);
}
