using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Newtonsoft.Json.Linq;
using TransposeApi;
using TransposeApi.Tunings.Guitar;

namespace TransposeUi;

public class ViewModel : INotifyPropertyChanged
{
    private string _tabText = "", _translatedText = "";
    public string TabText {get => _tabText; set => SetField(ref _tabText, value); }
    public string TranslatedText {get => _translatedText; set => SetField(ref _translatedText, value); }
    
    // --- -
    public void Translate()
    {
        if(string.IsNullOrEmpty(TabText)) return;
        var a = new Arrangement(TabText.Split("\n"), GuitarTunings.Standard);
        TranslatedText = a.ToSciPitchNotation();
    }

    public void SaveFile()
    {
        FileManager.SaveFile(new FileManager.SerializedFile()
        {
            Tuning =  GuitarTunings.Standard,
            BandName = "Test",
            SongName =  "Test Song",
            RawTab = TabText
        });
    }

    public async void LoadFile(TopLevel? topLevel)
    {
        var startDir = await topLevel.StorageProvider.TryGetFolderFromPathAsync(FileManager.GetAppDataPath());
        
        // Start async operation to open the dialog.
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Transpose File",
            AllowMultiple = false,
            SuggestedStartLocation = startDir
        });

        if (files.Count >= 1)
        {
            // Open reading stream from the first file.
            await using var stream = await files[0].OpenReadAsync();
            using var streamReader = new StreamReader(stream);
            // Reads all the content of file as a text.
            var fileContent = await streamReader.ReadToEndAsync();
            var deserialized = JObject.Parse(fileContent);
            var toArrg = deserialized.ToObject<FileManager.SerializedFile>();
            if(toArrg == null) return;
            TabText = toArrg.RawTab;
            Translate();
        }  
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

