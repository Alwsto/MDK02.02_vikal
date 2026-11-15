using AIS.UI.ViewModels;
using System.Windows;

namespace AIS.UI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new WorkloadViewModel(App.WorkloadService);
    }
}
