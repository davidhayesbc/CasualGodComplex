using System;
using System.Numerics;

namespace CasualGodComplex;

public class Star
{
    public Star(Vector3 position, string name, double temp, double mass, double radius, StarSequenceEnum sequence)
    {
        Name = name;
        Position = position;
        Temperature = temp;
        Mass = mass;
        Radius = radius;
        Sequence = sequence;
    }

    public Vector3 Position { get; internal set; }

    public double Radius { get; }
    public string Name { get; private set; }

    public double Temperature { get; }
    public double Mass { get; }
    public StarSequenceEnum Sequence { get; }
}

public class GalaxySeed
{
    public GalaxySeed(int seed)
    {
        GalaxyRandom = new Random(seed);
    }

    public Random GalaxyRandom { get; }
}

public class StarFactory
{
    private readonly GalaxySeed _seed;

    public StarFactory(GalaxySeed seed)
    {
        _seed = seed;
    }

    public Star CreateRandomStar(Vector3 position)
    {
        var starSequence = GetRandomStarType();


        return CreateRandomStar(starSequence, position);
    }

    private StarType GetRandomStarType()
    {
        var starTypePercentage = _seed.GalaxyRandom.NextDouble() * 100;
        var starSequence = starTypePercentage switch
                           {
                               >= 0 and <= 0.00003 => StarType.O,
                               > 0.00003 and <= 0.13003 => StarType.B,
                               > 0.13003 and <= 0.73003 => StarType.A,
                               > 0.73003 and <= 3.73003 => StarType.F,
                               > 3.73003 and <= 11.33003 => StarType.G,
                               > 11.33003 and <= 23.43003 => StarType.K,
                               > 23.43003 and <= 100 => StarType.M,
                               _ => throw new ArgumentOutOfRangeException()
                           };
        return starSequence;
    }

    public Star CreateRandomStar(StarType starType, Vector3 position)
    {
        var temperature = _seed.GalaxyRandom.Next(starType.ColorTemperatureRange);
        var mass = _seed.GalaxyRandom.NextDouble(starType.MassRange);
        var radius = _seed.GalaxyRandom.NextDouble(starType.RadiusRange);
        var name = StarName.Generate(_seed.GalaxyRandom);

        return new Star(position, name, temperature, mass, radius, starType.Sequence);
    }
}