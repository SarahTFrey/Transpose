using TransposeApi;
using TransposeApi.Tunings.Guitar;

namespace TransposeApiTests;

public class Tests
{
    private readonly Transposer _transposer = new ();
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void MidiToSciPitchNotation()
    {
        Assert.That(_transposer.MidiToSciPitchNotation(12), Is.EqualTo("C0"));
        Assert.That(_transposer.MidiToSciPitchNotation(69), Is.EqualTo("A4"));
        Assert.That(_transposer.MidiToSciPitchNotation(59), Is.EqualTo("B3"));
        Assert.That(_transposer.MidiToSciPitchNotation(61), Is.EqualTo("C#4"));
        Assert.That(_transposer.MidiToSciPitchNotation(58), Is.EqualTo("A#3"));
        Assert.That(_transposer.MidiToSciPitchNotation(108), Is.EqualTo("C8"));
        Assert.That(_transposer.MidiToSciPitchNotation(127), Is.EqualTo("G9"));
    }

    [Test]
    public void SciPitchNotationToMidi()
    {
        Assert.That(_transposer.SciPitchNotationToMidi("C0"), Is.EqualTo(12));
        Assert.That(_transposer.SciPitchNotationToMidi("A4"), Is.EqualTo(69));
        Assert.That(_transposer.SciPitchNotationToMidi("B3"), Is.EqualTo(59));
        Assert.That(_transposer.SciPitchNotationToMidi("C#4"), Is.EqualTo(61));
        Assert.That(_transposer.SciPitchNotationToMidi("A#3"), Is.EqualTo(58));
        Assert.That(_transposer.SciPitchNotationToMidi("C8"), Is.EqualTo(108));
        Assert.That(_transposer.SciPitchNotationToMidi("G9"), Is.EqualTo(127));
    }

    [Test]
    public void TabValueToSciPitchNotation()
    {
        // Standard Tuning
        Assert.That(_transposer.TabValueToSciPitchNotation("E4", 0), Is.EqualTo("E4"));
        Assert.That(_transposer.TabValueToSciPitchNotation("B3", 0), Is.EqualTo("B3"));
        Assert.That(_transposer.TabValueToSciPitchNotation("G3", 0), Is.EqualTo("G3"));
        Assert.That(_transposer.TabValueToSciPitchNotation("D3", 0), Is.EqualTo("D3"));
        Assert.That(_transposer.TabValueToSciPitchNotation("A2", 0), Is.EqualTo("A2"));
        Assert.That(_transposer.TabValueToSciPitchNotation("E2", 0), Is.EqualTo("E2"));
    }

    [Test]
    public void Arrangement()
    {
        var a = new Arrangement(Data.Split('\n'), GuitarTunings.Standard);
        var b = 1;
    }



    private string Data = @"
        Tuning: EADGBe
[Piano arranged for guitar]
e|---------------------------------|
B|---------------------------------|
G|---3---3---3---3---3---3---3---3-|
D|-----5-------5-------5-------5---|
A|-4-------4-------4-------4-------|
E|---------------------------------|
    ";
}