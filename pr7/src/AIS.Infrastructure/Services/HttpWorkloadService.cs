using System.Net.Http.Json;
using AIS.Core.Models;
using AIS.Core.Services;

namespace AIS.Infrastructure.Services;

public sealed class HttpWorkloadService(HttpClient httpClient) : IWorkloadService
{
    public async Task<IReadOnlyList<WorkloadItem>> GetWorkloadsAsync(CancellationToken cancellationToken = default) =>
        await httpClient.GetFromJsonAsync<List<WorkloadItem>>("api/v1/workloads", cancellationToken) ?? [];
}
