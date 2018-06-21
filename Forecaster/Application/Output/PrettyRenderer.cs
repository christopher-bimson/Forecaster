using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BetterConsoleTables;
using Forecaster.Core.Model.Summary;

namespace Forecaster.Application.Output
{
    public class PrettyRenderer : IRenderer
    {
        private TextWriter writer;

        public PrettyRenderer(TextWriter writer)
        {
            this.writer = writer;
        }

        public void Render(IEnumerable<Bucket> summarizedForecast)
        {
            var table = new Table("Likelihood", "At Least")
            {
                Config = TableConfiguration.MySql()
            };
            foreach (var bucket in summarizedForecast.OrderByDescending(b => b.Likelihood))
            {
                table.AddRow(bucket.Likelihood, bucket.Value);
            }
            writer.Write(table.ToStringWithoutExcessiveWhitespace());
        }
    }
}
