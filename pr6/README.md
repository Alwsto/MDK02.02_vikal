# ПР6 — клиент MVVM и OpenAPI

Продолжение ПР1–ПР5 для варианта №5, АИС «Управление студенческой нагрузкой».

## Состав

- `src/StudentLoad.Api/` — ASP.NET Core API с `GET /api/v1/workloads`;
- `src/StudentLoad.Client/` — WPF-клиент на MVVM;
- `openapi.yaml` — спецификация API;
- `docs/mvvm.md` — назначение View, ViewModel и Model.

## Запуск

Требуется .NET 8 SDK

1. В первом терминале: `dotnet run --project src/StudentLoad.Api`.
2. Во втором терминале: `dotnet run --project src/StudentLoad.Client`.

