using System.Diagnostics.CodeAnalysis;

namespace Budgy
{
    public class SalesData
    {
        public required string Month { get; init; }
        public required double Value { get; init; }

        [SetsRequiredMembers]
        public SalesData(string month, double value)
        {
            Month = month;
            Value = value;
        }
    }
}