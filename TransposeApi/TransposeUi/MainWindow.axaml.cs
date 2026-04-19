using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;

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
    private void SaveFile(object? sender, RoutedEventArgs e) => ViewModel.SaveFile();

    private async void LoadFile(object? sender, RoutedEventArgs e)
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        ViewModel.LoadFile(Window.GetTopLevel(this));
    } 
}