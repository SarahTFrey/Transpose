namespace TransposeApi.Tunings.Guitar;

public static class GuitarTunings
{
    // string to midi
    public static readonly Dictionary<string, byte> Standard = new () {
        {"e", 64},
        {"B", 59},
        {"G", 55},
        {"D", 50},
        {"A", 45},
        {"E", 40},
    };
}