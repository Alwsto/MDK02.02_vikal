using AIS.Core.Models;
using AIS.Core.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AIS.UI.ViewModels;

public sealed class WorkloadViewModel : INotifyPropertyChanged
{
    private readonly IWorkloadService _service;
    private string _searchText = "";
    private string _statusMessage = "Готово к загрузке данных.";
    private bool _isBusy;

    public WorkloadViewModel(IWorkloadService service)
    {
        _service = service;
        RefreshCommand = new AsyncCommand(RefreshAsync);
        DemoCommand = new RelayCommand(LoadDemo);
    }

    public ObservableCollection<WorkloadItem> Items { get; } = [];
    public IEnumerable<WorkloadItem> FilteredItems => string.IsNullOrWhiteSpace(SearchText) ? Items : Items.Where(x => x.TeacherName.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase));
    public string SearchText { get => _searchText; set { if (Set(ref _searchText, value)) NotifyCounts(); } }
    public string StatusMessage { get => _statusMessage; private set => Set(ref _statusMessage, value); }
    public bool IsBusy { get => _isBusy; private set => Set(ref _isBusy, value); }
    public string FooterMessage => $"Записей: {FilteredItems.Count()}";
    public ICommand RefreshCommand { get; }
    public ICommand DemoCommand { get; }

    private async Task RefreshAsync()
    {
        IsBusy = true; StatusMessage = "Загрузка из API…";
        try { var rows = await _service.GetWorkloadsAsync(); Items.Clear(); foreach (var row in rows) Items.Add(row); StatusMessage = "Данные успешно обновлены."; }
        catch (HttpRequestException) { StatusMessage = "API недоступен. Запустите AIS.Api или выберите демо-данные."; }
        catch (Exception ex) { StatusMessage = $"Ошибка загрузки: {ex.Message}"; }
        finally { IsBusy = false; NotifyCounts(); }
    }

    private void LoadDemo()
    {
        Items.Clear();
        Items.Add(new("tch-001", "Иванова Мария Сергеевна", "Информатика", "2026-2027", 612, "Рассчитано"));
        Items.Add(new("tch-002", "Петров Алексей Олегович", "Математика", "2026-2027", 548, "Требует проверки"));
        Items.Add(new("tch-003", "Смирнова Елена Павловна", "Информатика", "2026-2027", 590, "Рассчитано"));
        StatusMessage = "Показаны локальные демонстрационные данные."; NotifyCounts();
    }

    private void NotifyCounts() { OnPropertyChanged(nameof(FilteredItems)); OnPropertyChanged(nameof(FooterMessage)); }
    public event PropertyChangedEventHandler? PropertyChanged;
    private bool Set<T>(ref T field, T value, [CallerMemberName] string? name = null) { if (EqualityComparer<T>.Default.Equals(field, value)) return false; field = value; OnPropertyChanged(name); return true; }
    private void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new(name));

    private sealed class RelayCommand(Action action) : ICommand
    {
        public event EventHandler? CanExecuteChanged { add { } remove { } }
        public bool CanExecute(object? parameter) => true;
        public void Execute(object? parameter) => action();
    }
    private sealed class AsyncCommand(Func<Task> action) : ICommand
    {
        public event EventHandler? CanExecuteChanged { add { } remove { } }
        public bool CanExecute(object? parameter) => true;
        public async void Execute(object? parameter) => await action();
    }
}
