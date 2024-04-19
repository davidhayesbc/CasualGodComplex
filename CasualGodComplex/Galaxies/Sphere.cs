using System;
using System.Collections.Generic;
using System.Numerics;

namespace CasualGodComplex.Galaxies;

public class Sphere
    : BaseGalaxySpec
{
    private readonly float _densityDeviation;

    private readonly float _densityMean;

    private readonly float _deviationX;
    private readonly float _deviationY;
    private readonly float _deviationZ;
    private readonly float _size;
    private readonly StarFactory _starFactory;

    public Sphere(GalaxySeed seed,
        float size,
        float densityMean = 0.0000025f, float densityDeviation = 0.000001f,
        float deviationX = 0.0000025f,
        float deviationY = 0.0000025f,
        float deviationZ = 0.0000025f
    ) : base(seed)
    {
        _size = size;

        _densityMean = densityMean;
        _densityDeviation = densityDeviation;

        _deviationX = deviationX;
        _deviationY = deviationY;
        _deviationZ = deviationZ;
        _starFactory = new StarFactory(seed);
    }

    protected internal override IEnumerable<Star> Generate()
    {
        var density = Math.Max(0, Seed.GalaxyRandom.NormallyDistributedSingle(_densityDeviation, _densityMean));
        var countMax = Math.Max(0, (int)(_size * _size * _size * density));
        if (countMax <= 0)
            yield break;

        var count = Seed.GalaxyRandom.Next(countMax);

        for (var i = 0; i < count; i++)
        {
            var position = new Vector3(
                Seed.GalaxyRandom.NormallyDistributedSingle(_deviationX * _size, 0),
                Seed.GalaxyRandom.NormallyDistributedSingle(_deviationY * _size, 0),
                Seed.GalaxyRandom.NormallyDistributedSingle(_deviationZ * _size, 0)
            );
            yield return _starFactory.CreateRandomStar(position);
        }
    }
}