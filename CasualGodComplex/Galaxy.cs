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


    public static async Task<Galaxy> Generate(BaseGalaxySpec spec)
    {
        var s = await Task.Factory.StartNew(() => spec.Generate());

        return new Galaxy(s);
    }
}