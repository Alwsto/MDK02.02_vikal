# МДК 02.02 — практические работы № 1–9

**Инструментальные средства разработки и отладки программного обеспечения**

Практические работы: Вихров Александр Алексеевич, группа И14-23-9, **вариант № 5**.  
АИС «Управление студенческой нагрузкой»: формирование учебных планов, расчёт часов преподавателей, отчётность, авторизация и клиентское приложение WPF.

Руководитель: Чудинова Александра Анатольевна.

| Работа | Тема | Материалы |
|---|---|---|
| № 1 | Анализ требований и первичное проектирование структуры репозитория | [отчёт ПР1](PR1/docs/Отчет_ПР1_Вихров.А.А.docx) |
| № 2 | Проектирование уровней интеграции и схемы взаимодействия модулей | [отчёт ПР2](pr2/Отчет_ПР2_Вихров.А.А.docx) |
| № 3 | Проектирование правил сопоставления данных | [отчёт ПР3](pr3/Отчет_ПР3_Вихров.А.А.docx) |
| № 4 | Проектирование контрактов данных и валидация авторизации | [ПР4](pr4/) |
| № 5 | Проектирование стратегии ветвления Git Flow для распределённой команды | [ПР5](pr5/) |
| № 6 | Проектирование клиентской архитектуры MVVM и OpenAPI-спецификации | [отчёт ПР6](pr6/Отчёт_ПР6_Вариант_5.md) |
| № 7 | Разработка диаграммы модулей и структуры проекта для АИС | [отчёт ПР7](pr7/Report_PR7.md) |
| № 8 | Разработка перечня артефактов и протоколов проекта | [отчёт ПР8](pr8/REPORT.md) |
| № 9 | Настройка `.gitignore` и прав доступа | [отчёт ПР9](pr9/REPORT.md) |

## Структура репозитория

```text
MDK02.02_vikal/
├── README.md
├── PR1/
│   ├── docs/
│   │   └── Отчет_ПР1_Вихров.А.А.docx
│   ├── src/
│   └── tests/
├── pr2/
│   └── Отчет_ПР2_Вихров.А.А.docx
├── pr3/
│   └── Отчет_ПР3_Вихров.А.А.docx
├── pr4/
│   ├── Program.cs
│   ├── StudentLoadAuth.csproj
│   ├── examples/
│   └── Отчет_ПР4_Вихров.А.А.docx
├── pr5/
│   ├── CONTRIBUTING.md
│   ├── ISSUE_TEMPLATE/
│   └── Readme.md
├── pr6/
│   ├── README.md
│   ├── Отчёт_ПР6_Вариант_5.md
│   ├── openapi.yaml
│   └── src/
│       ├── StudentLoad.Api/
│       └── StudentLoad.Client/
├── pr7/
│   ├── AIS.StudentLoad.sln
│   ├── Report_PR7.md
│   ├── src/
│   │   ├── AIS.Api/
│   │   ├── AIS.Core/
│   │   ├── AIS.Infrastructure/
│   │   └── AIS.UI/
│   └── tests/AIS.Tests/
├── pr8/
│   ├── REPORT.md
│   └── artifacts/
└── pr9/
    ├── REPORT.md
    └── config/branch-protection-main.json
```

## Ключевые материалы

| Назначение | Расположение |
|---|---|
| Учебный прототип авторизации | [pr4/Program.cs](pr4/Program.cs) |
| Примеры JSON-запросов и ответов авторизации | [pr4/examples](pr4/examples/) |
| Контракт API | [pr6/openapi.yaml](pr6/openapi.yaml) |
| ASP.NET Core API ПР6 | [pr6/src/StudentLoad.Api](pr6/src/StudentLoad.Api/) |
| WPF-клиент MVVM ПР6 | [pr6/src/StudentLoad.Client](pr6/src/StudentLoad.Client/) |
| .NET Solution | [pr7/AIS.StudentLoad.sln](pr7/AIS.StudentLoad.sln) |
| Доменное ядро | [pr7/src/AIS.Core](pr7/src/AIS.Core/) |
| Инфраструктурный слой | [pr7/src/AIS.Infrastructure](pr7/src/AIS.Infrastructure/) |
| WPF UI | [pr7/src/AIS.UI](pr7/src/AIS.UI/) |
| ASP.NET Core API ПР7 | [pr7/src/AIS.Api](pr7/src/AIS.Api/) |
| Проверки доменной логики | [pr7/tests/AIS.Tests](pr7/tests/AIS.Tests/) |
| Паспорт артефактов и ADR | [pr8/REPORT.md](pr8/REPORT.md) |
| Конфигурация защиты `main` | [pr9/config/branch-protection-main.json](pr9/config/branch-protection-main.json) |

## Как запустить

Для запуска приложений требуется .NET SDK 8.0. WPF-клиент запускается в Windows.

### ПР6 — API и клиент

В первом окне PowerShell:

```powershell
cd pr6
dotnet run --project src/StudentLoad.Api/StudentLoad.Api.csproj
```

Во втором окне PowerShell:

```powershell
cd pr6
dotnet run --project src/StudentLoad.Client/StudentLoad.Client.csproj
```

### ПР7 — Solution, API и проверки Core

```powershell
cd pr7
dotnet build AIS.StudentLoad.sln
dotnet run --project tests/AIS.Tests/AIS.Tests.csproj
```

Для запуска API и WPF-клиента откройте два отдельных окна PowerShell:

```powershell
cd pr7
dotnet run --project src/AIS.Api/AIS.Api.csproj
```

```powershell
cd pr7
dotnet run --project src/AIS.UI/AIS.UI.csproj
```

> Если локальный API уже использует порт, завершите ранее запущенный процесс API перед запуском следующего.

### ПР9 — Git LFS

После добавления правил `.gitattributes` в корень репозитория:

```powershell
git lfs install
git check-attr filter -- README.md example.docx screenshot.png
git lfs ls-files
git status
```

## Требования

- .NET SDK 8.0;
- Windows для WPF-клиента;
- Git и Git LFS для материалов ПР5 и ПР9.
