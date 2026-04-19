namespace TransposeApi;

public class Transposer // Determines note value
{
    private readonly string[] _notes = ["C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B"];

    public string MidiToSciPitchNotation(uint midiPitch) => midiPitch <= byte.MaxValue
        ? MidiToSciPitchNotation(midiPitch)
        : throw new ArgumentOutOfRangeException(nameof(midiPitch));

    public string MidiToSciPitchNotation(byte midiPitch) =>
        $"{_notes[midiPitch % 12]}{(byte)Math.Floor((double)midiPitch / 12) - 1}";

    public byte SciPitchNotationToMidi(string sciPitchNotation) => sciPitchNotation.Length switch
    {
        2 => (byte)(((int.Parse(sciPitchNotation[1].ToString()) + 1) * 12) + _notes.IndexOf(sciPitchNotation[0].ToString())),
        3 => (byte)(((int.Parse(sciPitchNotation[2].ToString()) + 1) * 12) + _notes.IndexOf(sciPitchNotation[..2])),
        _ => throw new ArgumentOutOfRangeException(nameof(sciPitchNotation))
    };

    public byte TabValueToMidi(byte rootNoteMidi, byte fretValue) => (byte)(rootNoteMidi + fretValue);

    public string TabValueToSciPitchNotation(byte rootNoteMidi, byte fretValue) =>
        MidiToSciPitchNotation((byte)(rootNoteMidi + fretValue));
    
    public string TabValueToSciPitchNotation(string rootNoteMidi, byte fretValue) =>
        MidiToSciPitchNotation((byte)(SciPitchNotationToMidi(rootNoteMidi) + fretValue));

    public (byte rootNote, byte fretValue) SciPitchNotationToTab(byte[] midiTuning, string sciPitchNotation) =>
        (0, 0); // TODO

    public (byte rootNote, byte freyValue) MidiToTab(byte[] midiTuning, byte midi) => (0, 0); // TODO
}