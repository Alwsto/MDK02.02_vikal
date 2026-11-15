# ОТЧЁТ ПО ПРАКТИЧЕСКОЙ РАБОТЕ № 9

## Настройка .gitignore и прав доступа

**Вариант № 5**  
**АИС «Управление студенческой нагрузкой»**

| Сведения | Значение |
|---|---|
| Дисциплина | МДК 02.02 Инструментальные средства разработки и отладки ПО |
| Студент | Вихров Александр Алексеевич |
| Группа | И14-23-9 |
| Вариант | 5 |
| Модуль ИС | АИС «Управление студенческой нагрузкой» |
| Руководитель | Чудинова Александра Анатольевна |
| Дата | «____» ______________ 20____ г. |

> При переносе отчёта в Word установить шрифт Times New Roman, 14 пт.

## Введение

Практическая работа № 9 продолжает проект АИС «Управление студенческой нагрузкой» из ПР1–ПР8. Цель работы — исключить из истории репозитория машинные артефакты и локальные данные, нормализовать текстовые файлы для разных операционных систем, настроить хранение тяжёлых файлов через Git LFS и описать защиту ветки `main`.

## 1 Листинг файла `.gitignore`

Файл размещается в корне проекта. Он исключает результаты сборки .NET, настройки IDE, тестовые результаты, локальные базы данных, логи и локальные секреты. Исходный код, файлы Solution, `appsettings.json` и пример конфигурации остаются под контролем версий.

```gitignore
# Build output
[Bb]in/
[Oo]bj/
[Dd]ebug/
[Rr]elease/
/publish/
/artifacts/

# Test output and coverage
TestResults/
coverage/
*.coverage
*.coveragexml
*.trx

# Visual Studio, Rider and VS Code
.vs/
.idea/
.vscode/
*.suo
*.user
*.userosscache
*.sln.docstates
*.rsuser

# NuGet and local tooling
packages/
*.nupkg
*.snupkg

# Local databases and generated data
*.db
*.db-shm
*.db-wal
*.sqlite
*.sqlite3
*.mdf
*.ldf

# Local configuration and secrets
.env
.env.*
!.env.example
appsettings.Development.json
appsettings.Local.json
secrets.json
!appsettings.json
!appsettings.Example.json

# Logs and operating-system files
*.log
/logs/
Thumbs.db
Desktop.ini
.DS_Store
```

## 2 Листинг файла `.gitattributes`

Первая строка нормализует окончания строк в текстовых файлах. PowerShell-скрипты остаются с CRLF для удобства работы в Windows. Крупные двоичные форматы подключены к Git LFS и не обрабатываются как текст.

```gitattributes
* text=auto eol=lf

*.sln text
*.cs text
*.csproj text
*.xaml text
*.md text
*.yml text
*.yaml text
*.json text
*.ps1 text eol=crlf

*.docx filter=lfs diff=lfs merge=lfs -text
*.pdf filter=lfs diff=lfs merge=lfs -text
*.pptx filter=lfs diff=lfs merge=lfs -text
*.xlsx filter=lfs diff=lfs merge=lfs -text
*.zip filter=lfs diff=lfs merge=lfs -text
*.7z filter=lfs diff=lfs merge=lfs -text
*.png filter=lfs diff=lfs merge=lfs -text
*.jpg filter=lfs diff=lfs merge=lfs -text
*.jpeg filter=lfs diff=lfs merge=lfs -text
*.gif filter=lfs diff=lfs merge=lfs -text
*.mp3 filter=lfs diff=lfs merge=lfs -text
*.mp4 filter=lfs diff=lfs merge=lfs -text
```

## 3 Защита ветки `main` и Git LFS

Для `main` подготовлены GitHub Actions workflow `build-and-check`, инструкция защиты ветки и JSON-шаблон правила. Право защиты ветки изменяется через **GitHub Settings → Branches**, поэтому его нельзя применить только добавлением файла в Git. В подключённом GitHub-репозитории доступна проверка содержимого, но изменение Branch Protection для текущего подключения недоступно; конфигурация подготовлена для применения владельцем репозитория.

Настройки защиты `main`:

- Pull Request обязателен перед слиянием;
- необходимо одно одобрение, а устаревшие одобрения сбрасываются при новом коммите;
- требуется разрешить все комментарии ревью;
- workflow `build-and-check` должен завершиться успешно, а ветка должна быть актуальна;
- линейная история обязательна;
- принудительная отправка и удаление `main` запрещены;
- запрет применяется также к администраторам, если настройка доступна.

Git LFS проверен локально. Он установлен, и команды `git lfs install --local` и `git lfs track` завершились успешно. Полный проверочный лог находится в [`logs/git-lfs-verification.md`](logs/git-lfs-verification.md).

После переноса файлов в основной репозиторий проверка выполняется командами:

```powershell
git status
git check-attr filter -- REPORT.md example.docx screenshot.png
git lfs ls-files
```

Подробные инструкции находятся в [`docs/git-lfs.md`](docs/git-lfs.md) и [`docs/branch-protection-main.md`](docs/branch-protection-main.md).

## Заключение

Для репозитория подготовлены `.gitignore` и `.gitattributes`, которые обеспечивают чистоту .NET-проекта, кроссплатформенную обработку текстовых файлов и хранение тяжёлых двоичных файлов через Git LFS. Добавлен workflow `build-and-check`, обеспечивающий проверку сборки и правил Core в Pull Request. Для ветки `main` зафиксированы обязательный Pull Request, ревью, успешная проверка workflow, разрешение комментариев и запрет опасных операций. Эти настройки поддерживают командный регламент, заданный в ПР5, и защищают кодовые артефакты ПР6–ПР8.
