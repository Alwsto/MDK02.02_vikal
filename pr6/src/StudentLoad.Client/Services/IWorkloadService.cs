using StudentLoad.Client.Models;

namespace StudentLoad.Client.Services;

public interface IWorkloadService
{
    Task<IReadOnlyList<WorkloadItem>> GetWorkloadsAsync(CancellationToken cancellationToken = default);
}
