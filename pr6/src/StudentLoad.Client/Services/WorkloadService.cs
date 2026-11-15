using System.Net.Http;
using System.Net.Http.Json;
using StudentLoad.Client.Models;

namespace StudentLoad.Client.Services;

public sealed class WorkloadService : IWorkloadService
{
    private readonly HttpClient _http = new() { BaseAddress = new Uri("http://localhost:5080/") };

    public async Task<IReadOnlyList<WorkloadItem>> GetWorkloadsAsync(CancellationToken cancellationToken = default)
    {
        using var response = await _http.GetAsync("api/v1/workloads", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<WorkloadItem>>(cancellationToken: cancellationToken) ?? [];
    }
}
