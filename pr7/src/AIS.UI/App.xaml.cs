using AIS.Core.Services;
using AIS.Infrastructure.Services;
using System.Net.Http;

namespace AIS.UI;

public partial class App : System.Windows.Application
{
    public static IWorkloadService WorkloadService { get; } = new HttpWorkloadService(
        new HttpClient { BaseAddress = new Uri("http://localhost:5080/") });
}
