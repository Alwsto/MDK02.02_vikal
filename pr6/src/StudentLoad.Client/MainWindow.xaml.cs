using System.Windows;
using StudentLoad.Client.ViewModels;

namespace StudentLoad.Client;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new WorkloadViewModel();
    }
}
