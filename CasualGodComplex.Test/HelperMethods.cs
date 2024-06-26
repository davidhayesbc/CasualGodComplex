﻿using System.IO;
using System.Reflection;
using System.Text;

namespace CasualGodComplex.Test;

public static class HelperMethods
{
    public static string To3JS(this Galaxy g)
    {
        var b = new StringBuilder();
        b.AppendLine("var stars = [");
        foreach (var star in g.Stars)
        {
            var c = StarColor.ConvertTemperature(star.Temperature);
            b.AppendLine($"  {{name:\"{star.Name}\",x:{star.Position.X},y:{star.Position.Y},z:{star.Position.Z},r:{c.X},g:{c.Y},b:{c.Z}}},");
        }

        b.AppendLine("];");

        b.AppendLine();

        var n = Assembly.GetExecutingAssembly().GetManifestResourceNames();

        using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CasualGodComplex.Test.ThreeJs.js"))
        using (var reader = new StreamReader(stream))
        {
            b.Append(reader.ReadToEnd());
        }

        return b.ToString();
    }
}