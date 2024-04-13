using System;
using System.Threading.Tasks;
using CasualGodComplex.Galaxies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CasualGodComplex.Test;

[TestClass]
public class Playground
{
    [TestMethod]
    public async Task Spiral()
    {
        var g = await Galaxy.Generate(new Spiral(), new Random(1));
        Console.WriteLine(g.To3JS());
    }

    [TestMethod]
    public void Name()
    {
        var r = new Random(1);
        for (var i = 0; i < 10; i++) Console.WriteLine(StarName.Generate(r));
    }

    [TestMethod]
    public void UniqueNames()
    {
        var names = string.Join(",\n", StarName.Generate(new Random(1), 1000));
    }
}