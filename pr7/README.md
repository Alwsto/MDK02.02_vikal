# ПР7 — структура решения АИС

## Состав

- `AIS.Core` — модель нагрузки, правило расчёта и интерфейсы;
- `AIS.Infrastructure` — реализация доступа к API;
- `AIS.UI` — WPF-клиент с MVVM;
- `AIS.Api` — HTTP API, согласованный с OpenAPI из ПР6;
- `AIS.Tests` — запускаемый набор проверок доменной логики без внешних test-пакетов.

## Зависимости

`AIS.Core` не ссылается на другие проекты. `AIS.Infrastructure` ссылается только на `AIS.Core`. WPF-клиент ссылается на Core и Infrastructure в точке сборки приложения. API ссылается на Core и Infrastructure. AIS.Tests ссылается только на Core.

Таким образом, правила и модели предметной области не зависят от UI, сети или конкретного хранилища. UI и API являются внешними слоями и подключают реализации Infrastructure при запуске.

## Запуск

Требуется .NET 8 SDK; WPF UI запускается в Windows.

```powershell
dotnet build AIS.StudentLoad.sln
dotnet run --project src/AIS.Api/AIS.Api.csproj
```

В отдельном терминале запустите WPF-клиент:

```powershell
dotnet run --project src/AIS.UI/AIS.UI.csproj
```

Чтобы выполнить проверки доменной логики:

```powershell
dotnet run --project tests/AIS.Tests/AIS.Tests.csproj
```

API слушает `http://localhost:5080`, endpoint: `GET /api/v1/workloads`.
