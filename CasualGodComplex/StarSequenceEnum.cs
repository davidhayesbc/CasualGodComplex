using System;

namespace CasualGodComplex;

public enum StarSequenceEnum
{
    O = 1,
    B,
    A,
    F,
    G,
    K,
    M
}

internal class StarType
{
    public StarType(StarSequenceEnum sequence)
    {
        Sequence = sequence;
        Frequency = Sequence switch
                    {
                        StarSequenceEnum.O => 0.0000003,
                        StarSequenceEnum.B => 0.013,
                        StarSequenceEnum.A => 0.006,
                        StarSequenceEnum.F => 0.003,
                        StarSequenceEnum.G => 0.076,
                        StarSequenceEnum.K => 0.121,
                        StarSequenceEnum.M => 0.7645,
                        _ => throw new ArgumentOutOfRangeException(nameof(sequence), sequence, "Sequence is not an allowable value")
                    };
        MassRange = Sequence switch
                    {
                        StarSequenceEnum.O => new Range<double>(16, 300),
                        StarSequenceEnum.B => new Range<double>(2.1, 16),
                        StarSequenceEnum.A => new Range<double>(1.4, 2.1),
                        StarSequenceEnum.F => new Range<double>(1.04, 1.4),
                        StarSequenceEnum.G => new Range<double>(0.8, 1.04),
                        StarSequenceEnum.K => new Range<double>(0.45, 0.8),
                        StarSequenceEnum.M => new Range<double>(0.08, 0.45),
                        _ => throw new ArgumentOutOfRangeException(nameof(sequence), sequence, "Sequence is not an allowable value")
                    };
        ColorTemperatureRange = Sequence switch
                                {
                                    StarSequenceEnum.O => new Range<int>(30_000, 40_000),
                                    StarSequenceEnum.B => new Range<int>(10_000, 30_000),
                                    StarSequenceEnum.A => new Range<int>(7_500, 10_000),
                                    StarSequenceEnum.F => new Range<int>(6_000, 7_500),
                                    StarSequenceEnum.G => new Range<int>(5_200, 6_000),
                                    StarSequenceEnum.K => new Range<int>(3_700, 5_200),
                                    StarSequenceEnum.M => new Range<int>(2_400, 3_700),
                                    _ => throw new ArgumentOutOfRangeException(nameof(sequence), sequence, "Sequence is not an allowable value")
                                };
        RadiusRange = Sequence switch
                      {
                          StarSequenceEnum.O => new Range<double>(6.6, 10),
                          StarSequenceEnum.B => new Range<double>(1.8, 6.6),
                          StarSequenceEnum.A => new Range<double>(1.4, 1.8),
                          StarSequenceEnum.F => new Range<double>(1.15, 1.4),
                          StarSequenceEnum.G => new Range<double>(0.96, 1.15),
                          StarSequenceEnum.K => new Range<double>(0.7, 0.96),
                          StarSequenceEnum.M => new Range<double>(0.5, 0.7),
                          _ => throw new ArgumentOutOfRangeException(nameof(sequence), sequence, "Sequence is not an allowable value")
                      };
    }

    public StarSequenceEnum Sequence { get; }
    public double Frequency { get; }
    public Range<double> MassRange { get; }
    public Range<int> ColorTemperatureRange { get; }
    public Range<double> RadiusRange { get; }

    public double FrequencyPercentage => Frequency * 100;
}

/// <summary>The Range structure.</summary>
/// <typeparam name="T">Generic parameter.</typeparam>
public readonly struct Range<T> where T : IComparable<T>
{
    public Range(T minimum, T maximum)
    {
        if (minimum.CompareTo(maximum) > 0) throw new ArgumentException("minimum must be less than or equal to maximum", nameof(minimum));
        Minimum = minimum;
        Maximum = maximum;
    }

    /// <summary>Minimum value of the range.</summary>
    public T Minimum { get; }

    /// <summary>Maximum value of the range.</summary>
    public T Maximum { get; }

    /// <summary>Presents the Range in readable format.</summary>
    /// <returns>String representation of the Range</returns>
    public override string ToString()
    {
        return $"[{Minimum} - {Maximum}]";
    }

    /// <summary>Determines if the provided value is inside the range.</summary>
    /// <param name="value">The value to test</param>
    /// <returns>True if the value is inside Range, else false</returns>
    public bool ContainsValue(T value)
    {
        return Minimum.CompareTo(value) <= 0 && value.CompareTo(Maximum) <= 0;
    }

    /// <summary>Determines if this Range is inside the bounds of another range.</summary>
    /// <param name="Range">The parent range to test on</param>
    /// <returns>True if range is inclusive, else false</returns>
    public bool IsInsideRange(Range<T> range)
    {
        return range.ContainsValue(Minimum) && range.ContainsValue(Maximum);
    }

    /// <summary>Determines if another range is inside the bounds of this range.</summary>
    /// <param name="Range">The child range to test</param>
    /// <returns>True if range is inside, else false</returns>
    public bool ContainsRange(Range<T> range)
    {
        return ContainsValue(range.Minimum) && ContainsValue(range.Maximum);
    }
}