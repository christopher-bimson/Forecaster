using BetterConsoleTables;
using System;
using System.Linq;

namespace Forecaster.Application.Output
{
    public static class TableExtensions
    {
        public static string ToStringWithoutExcessiveWhitespace(this Table table)
        {
            return String.Join(Environment.NewLine, table.ToString()
                .Split(Environment.NewLine)
                .Select(s => s.Trim()));
        }
    }
}
