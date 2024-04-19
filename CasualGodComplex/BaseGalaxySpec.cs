using System.Collections.Generic;

namespace CasualGodComplex;

public abstract class BaseGalaxySpec
{
    public BaseGalaxySpec(GalaxySeed seed)
    {
        Seed = seed;
    }

    public GalaxySeed Seed { get; }

    protected internal abstract IEnumerable<Star> Generate();
}