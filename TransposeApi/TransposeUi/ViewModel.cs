using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TransposeUi;

public class ViewModel : INotifyPropertyChanged
{
    private string _tabText = "";
    public string TabText {get => _tabText; set => SetField(ref _tabText, value); }

    
    // --- -
    public void Translate()
    {
        
    }
    
    // -----
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}

