using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasualGodComplex;

public class Galaxy
{
    private Galaxy(IEnumerable<Star> stars)
    {
        Stars = stars;
    }

    public IEnumerable<Star> Stars { get; private set; }


    public static async Task<Galaxy> Generate(BaseGalaxySpec spec, Random random)
    {
        var s = await Task.Factory.StartNew(() => spec.Generate(random));

        return new Galaxy(s);
    }
}