namespace CasualGodComplex;

public class Planet
{
    public Planet(string name, float orbitalRadius, float planetaryRadius)
    {
        OrbitalRadius = orbitalRadius;
        Name = name;
    }

    public string Name { get; private set; }

    public float OrbitalRadius { get; private set; }
}