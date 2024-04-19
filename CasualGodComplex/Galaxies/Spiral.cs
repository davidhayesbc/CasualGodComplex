using System;
using System.Collections.Generic;
using System.Numerics;

namespace CasualGodComplex.Galaxies;

public class Spiral
    : BaseGalaxySpec
{
    public Spiral(GalaxySeed seed) : base(seed)
    {
        Size = 750;
        Spacing = 5;

        MinimumArms = 3;
        MaximumArms = 7;

        ClusterCountDeviation = 0.35f;
        ClusterCenterDeviation = 0.2f;

        MinArmClusterScale = 0.02f;
        ArmClusterScaleDeviation = 0.02f;
        MaxArmClusterScale = 0.1f;

        Swirl = (float)Math.PI * 4;

        CenterClusterScale = 0.19f;
        CenterClusterDensityMean = 0.00005f;
        CenterClusterDensityDeviation = 0.000005f;
        CenterClusterSizeDeviation = 0.00125f;

        CenterClusterCountMean = 20;
        CenterClusterCountDeviation = 3;
        CenterClusterPositionDeviation = 0.075f;

        CentralVoidSizeMean = 25;
        CentralVoidSizeDeviation = 7;
    }

    /// <summary>
    ///     Approximate physical size of the galaxy
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    ///     Approximate spacing between clusters
    /// </summary>
    public int Spacing { get; set; }

    /// <summary>
    ///     Minimum number of arms
    /// </summary>
    public int MinimumArms { get; set; }

    /// <summary>
    ///     Maximum number of arms
    /// </summary>
    public int MaximumArms { get; set; }

    public float ClusterCountDeviation { get; set; }
    public float ClusterCenterDeviation { get; set; }

    public float MinArmClusterScale { get; set; }
    public float ArmClusterScaleDeviation { get; set; }
    public float MaxArmClusterScale { get; set; }

    public float Swirl { get; set; }

    public float CenterClusterScale { get; set; }
    public float CenterClusterDensityMean { get; set; }
    public float CenterClusterDensityDeviation { get; set; }
    public float CenterClusterSizeDeviation { get; set; }

    public float CenterClusterPositionDeviation { get; set; }
    public float CenterClusterCountDeviation { get; set; }
    public float CenterClusterCountMean { get; set; }

    public float CentralVoidSizeMean { get; set; }
    public float CentralVoidSizeDeviation { get; set; }

    protected internal override IEnumerable<Star> Generate()
    {
        var centralVoidSize = Seed.GalaxyRandom.NormallyDistributedSingle(CentralVoidSizeDeviation, CentralVoidSizeMean);
        if (centralVoidSize < 0)
            centralVoidSize = 0;
        var centralVoidSizeSqr = centralVoidSize * centralVoidSize;

        foreach (var star in GenerateArms())
            if (star.Position.LengthSquared() > centralVoidSizeSqr)
                yield return star;

        foreach (var star in GenerateCenter())
            if (star.Position.LengthSquared() > centralVoidSizeSqr)
                yield return star;

        foreach (var star in GenerateBackgroundStars())
            if (star.Position.LengthSquared() > centralVoidSizeSqr)
                yield return star;
    }

    private IEnumerable<Star> GenerateBackgroundStars()
    {
        return new Sphere(Seed, Size, 0.000001f, 0.0000001f, 0.35f, 0.125f, 0.35f).Generate();
    }

    private IEnumerable<Star> GenerateCenter()
    {
        //Add a single central cluster
        var sphere = new Sphere(Seed,
            Size * CenterClusterScale,
            CenterClusterDensityMean,
            CenterClusterDensityDeviation,
            CenterClusterScale,
            CenterClusterScale,
            CenterClusterScale
        );

        var cluster = new Cluster(sphere,
            CenterClusterCountMean, CenterClusterCountDeviation, Size * CenterClusterPositionDeviation, Size * CenterClusterPositionDeviation, Size * CenterClusterPositionDeviation
        );

        foreach (var star in cluster.Generate())
            yield return star.Swirl(Vector3.UnitY, Swirl * 5);
    }

    private IEnumerable<Star> GenerateArms()
    {
        var arms = Seed.GalaxyRandom.Next(MinimumArms, MaximumArms);
        var armAngle = (float)(Math.PI * 2 / arms);

        var maxClusters = Size / Spacing / arms;
        for (var arm = 0; arm < arms; arm++)
        {
            var clusters = (int)Math.Round(Seed.GalaxyRandom.NormallyDistributedSingle(maxClusters * ClusterCountDeviation, maxClusters));
            for (var i = 0; i < clusters; i++)
            {
                //Angle from center of this arm
                var angle = Seed.GalaxyRandom.NormallyDistributedSingle(0.5f * armAngle * ClusterCenterDeviation, 0) + armAngle * arm;

                //Distance along this arm
                var dist = Math.Abs(Seed.GalaxyRandom.NormallyDistributedSingle(Size * 0.4f, 0));

                //Center of the cluster
                var center = Vector3.Transform(new Vector3(0, 0, dist), Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), angle));

                //Size of the cluster
                var clsScaleDev = ArmClusterScaleDeviation * Size;
                var clsScaleMin = MinArmClusterScale * Size;
                var clsScaleMax = MaxArmClusterScale * Size;
                var cSize = Seed.GalaxyRandom.NormallyDistributedSingle(clsScaleDev, clsScaleMin * 0.5f + clsScaleMax * 0.5f, clsScaleMin, clsScaleMax);

                var stars = new Sphere(Seed, Size, 0.00025f, deviationX: 1, deviationY: 1, deviationZ: 1).Generate();
                foreach (var star in stars)
                    yield return star.Offset(center).Swirl(Vector3.UnitY, Swirl);
            }
        }
    }
}