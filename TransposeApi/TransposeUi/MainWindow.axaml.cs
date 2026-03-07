using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TransposeUi;

public partial class MainWindow : Window
{
    private ViewModel ViewModel {get; set;}
    public MainWindow()
    {
        InitializeComponent();
        DataContext = ViewModel = new ViewModel();
    }

    private void TranslateTabs(object? sender, RoutedEventArgs e) => ViewModel.Translate();

}