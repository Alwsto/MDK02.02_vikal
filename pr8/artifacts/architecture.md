# Архитектурный артефакт

АИС «Управление студенческой нагрузкой» разделена на доменное ядро, инфраструктурные реализации и внешние точки входа. Основное правило: Core не зависит от UI, API или Infrastructure.

```mermaid
flowchart LR
    UI[AIS.UI — WPF / MVVM] --> Core[AIS.Core — модели, правила, интерфейсы]
    Infra[AIS.Infrastructure — HTTP и источники данных] --> Core
    UI --> Infra
    API[AIS.Api — ASP.NET Core] --> Core
    API --> Infra
    Tests[AIS.Tests] --> Core
```

Core содержит `WorkloadItem`, `IWorkloadService` и `WorkloadRules`. Infrastructure реализует получение данных через HTTP для клиента и демонстрационный источник в памяти для API. UI содержит View и ViewModel, API предоставляет `GET /api/v1/workloads`, а Tests проверяет правила Core.

Изменения зависимостей и состава проектов документируются вместе с соответствующим Pull Request и при необходимости новым ADR.
