
using System;
using System.Collections.Generic;

namespace Forecaster.Domain
{
    static class OutputWriterFactory
    {
        private static readonly IDictionary<ForecastOutputFormat, IOutputWriter> OutputWriters
            = new Dictionary<ForecastOutputFormat, IOutputWriter>
            {
                {ForecastOutputFormat.Pretty, new PrettyOutputWriter() },
                {ForecastOutputFormat.Json, new JsonOutputWriter(Console.Out) },
                {ForecastOutputFormat.Csv, new CsvOutputWriter(Console.Out) },
            };

        internal static IOutputWriter Create(ForecastOutputFormat format)
        {
            return OutputWriters[format];
        }
    }
}
