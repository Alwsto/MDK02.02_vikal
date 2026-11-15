# Кодовой артефакт

## Решение

Исходный проект ПР7 расположен в `src/` и `tests/`, решение называется `AIS.StudentLoad.sln`.

| Проект | Содержимое |
|---|---|
| `AIS.Core` | `Models/WorkloadItem.cs`, `Services/IWorkloadService.cs`, `Rules/WorkloadRules.cs` |
| `AIS.Infrastructure` | `Services/HttpWorkloadService.cs`, `Services/InMemoryWorkloadService.cs` |
| `AIS.UI` | WPF View, `ViewModels/WorkloadViewModel.cs`, композиция приложения |
| `AIS.Api` | ASP.NET Core endpoint `GET /api/v1/workloads` |
| `AIS.Tests` | Самостоятельно запускаемые проверки доменных правил |

## Ключевые команды

```powershell
dotnet build AIS.StudentLoad.sln
dotnet run --project src/AIS.Api/AIS.Api.csproj
dotnet run --project src/AIS.UI/AIS.UI.csproj
dotnet run --project tests/AIS.Tests/AIS.Tests.csproj
```

WPF-клиент предназначен для Windows. Демонстрационный API использует источник в памяти; секреты и реальные данные в репозиторий не добавляются.
