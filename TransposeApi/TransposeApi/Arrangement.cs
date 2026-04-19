namespace TransposeApi;

public class Arrangement
{
    private readonly Transposer _transposer = new();
    private readonly List<List<byte?>> _midiValue = [];
    private readonly List<ushort> _tabSectionStartIndexes = new();

    public Arrangement(string[] input, Dictionary<string, byte> guitarTuning)
    {
        var fretValue = 0;
        byte? stringMidiValue = null;

        var count = -1;
        
        foreach (var line in input)
        {
            if (line.Length < 2) continue;
            count++;
            _midiValue.Add(new List<byte?>());
            foreach (var c in line.Replace('p', '-').Replace('h', '-').Replace('~', '-').Replace('(', '-').Replace(')', '-').Replace('r', '-').Split('-'))
            {
                if (stringMidiValue == null && c.Contains('|') &&
                    guitarTuning.TryGetValue(c[0].ToString(), out var val))
                {
                    stringMidiValue = val;
                    if(val == guitarTuning.Values.First()) _tabSectionStartIndexes.Add((ushort)count);
                }
                else if (stringMidiValue != null && int.TryParse(c, out fretValue))
                {
                    _midiValue.Last().Add(_transposer.TabValueToMidi(stringMidiValue.Value, (byte)fretValue));
                }
                else if(stringMidiValue == null)
                {
                    continue;
                }
                _midiValue.Last().Add(null);
            }

            stringMidiValue = null;
        }
    }

    public string ToSciPitchNotation()
    {
        if(!_tabSectionStartIndexes.Any()) return "No Tabs Found :(";
        var ret = "";
        int count = -1;
        foreach (var line in _midiValue)
        {
            count++;
            if(count < _tabSectionStartIndexes.First()) continue;
            if (_tabSectionStartIndexes.Contains((ushort)count))
            {
                ret += Environment.NewLine;
                ret += $"[Tab {_tabSectionStartIndexes.IndexOf((ushort)count) + 1}]";
                ret += Environment.NewLine;
            }
            foreach (var b in line)
            {
                if (b != null)
                {
                    var next = _transposer.MidiToSciPitchNotation(b.Value);
                    ret += next.PadRight(3, '-');
                }
                else
                {
                    ret += "---";
                }
            }
            ret += Environment.NewLine;
        }

        return ret;
    }

    private struct LineItem(List<byte?>? line, string? rawText)
    {
        private List<byte?>? _midiValue = line;
        private string? _rawText = rawText;

        public override string ToString()
        {
            var t = new Transposer();
            var ret = "";
            if (_rawText != null) ret += _rawText;
            if (_midiValue != null)
            {
                ret += _midiValue.Select(x => x == null ? "---" : t.MidiToSciPitchNotation(x.Value).PadLeft(3, '-'));
            }
            return ret;
        }
    }
}