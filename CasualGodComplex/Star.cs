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
        var starType = _seed.GalaxyRandom.NextDouble();

        return CreateRandomStar(StarSequenceEnum.O, position);
    }

    public Star CreateRandomStar(StarSequenceEnum sequence, Vector3 position)
    {
        return new Star(position, "", 1.0, 1, 1, sequence);
    }
}