using System;

namespace CasualGodComplex;

/// <summary>The ValueRange structure.</summary>
/// <typeparam name="T">Generic parameter.</typeparam>
public readonly struct ValueRange<T> where T : IComparable<T>
{
    public ValueRange(T minimum, T maximum)
    {
        if (minimum.CompareTo(maximum) > 0) throw new ArgumentException("minimum must be less than or equal to maximum", nameof(minimum));
        Minimum = minimum;
        Maximum = maximum;
    }

    /// <summary>Minimum value of the range.</summary>
    public T Minimum { get; }

    /// <summary>Maximum value of the range.</summary>
    public T Maximum { get; }

    /// <summary>Presents the ValueRange in readable format.</summary>
    /// <returns>String representation of the ValueRange</returns>
    public override string ToString()
    {
        return $"[{Minimum} - {Maximum}]";
    }

    /// <summary>Determines if the provided value is inside the range.</summary>
    /// <param name="value">The value to test</param>
    /// <returns>True if the value is inside ValueRange, else false</returns>
    public bool ContainsValue(T value)
    {
        return Minimum.CompareTo(value) <= 0 && value.CompareTo(Maximum) <= 0;
    }

    /// <summary>Determines if this ValueRange is inside the bounds of another range.</summary>
    /// <param name="Range">The parent range to test on</param>
    /// <returns>True if range is inclusive, else false</returns>
    public bool IsInsideRange(ValueRange<T> range)
    {
        return range.ContainsValue(Minimum) && range.ContainsValue(Maximum);
    }

    /// <summary>Determines if another range is inside the bounds of this range.</summary>
    /// <param name="Range">The child range to test</param>
    /// <returns>True if range is inside, else false</returns>
    public bool ContainsRange(ValueRange<T> range)
    {
        return ContainsValue(range.Minimum) && ContainsValue(range.Maximum);
    }
}