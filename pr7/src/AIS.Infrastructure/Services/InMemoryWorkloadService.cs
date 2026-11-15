using AIS.Core.Models;
using AIS.Core.Services;

namespace AIS.Infrastructure.Services;

public sealed class InMemoryWorkloadService : IWorkloadService
{
    private readonly IReadOnlyList<WorkloadItem> _items =
    [
        new("tch-001", "Иванова Мария Сергеевна", "Информатика", "2026-2027", 612, "Рассчитано"),
        new("tch-002", "Петров Алексей Олегович", "Математика", "2026-2027", 548, "Требует проверки"),
        new("tch-003", "Смирнова Елена Павловна", "Информатика", "2026-2027", 590, "Рассчитано")
    ];

    public Task<IReadOnlyList<WorkloadItem>> GetWorkloadsAsync(CancellationToken cancellationToken = default) => Task.FromResult(_items);
}
