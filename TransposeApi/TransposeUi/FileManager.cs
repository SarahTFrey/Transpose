using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TransposeApi;

namespace TransposeUi;

public static class FileManager
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SerializedFile
    {
        [JsonProperty("s")] public string SongName { get; set; } = "";
        [JsonProperty("b")] public string BandName { get; set; } = "";
        [JsonProperty("r")] public string RawTab { get; set; } = "";
        [JsonProperty("t")] public Dictionary<string, byte> Tuning { get; set; } = new();

        public Arrangement ToArrangement() => new(RawTab.Split(Environment.NewLine), Tuning);
    }

    private static readonly string RootDirectory = GetAppDataPath();
    
    public static string GetAppDataPath()
    {
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Transpose");
        if(!Directory.Exists(path)) Directory.CreateDirectory(path);
        return path;
    }
    
    public static bool SaveFile(SerializedFile file)
    {
        try
        {
            File.WriteAllText(Path.Combine(RootDirectory, $"{file.BandName}_{file.SongName}"), JsonConvert.SerializeObject(file));
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}