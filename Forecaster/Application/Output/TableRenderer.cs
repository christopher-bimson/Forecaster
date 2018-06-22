using System.Collections.Generic;
using System.IO;
using System.Linq;
using BetterConsoleTables;
using Forecaster.Core.Model.Summary;

namespace Forecaster.Application.Output
{
    public abstract class TableRenderer : IRenderer
    {
        protected readonly TextWriter writer;

        public TableRenderer(TextWriter writer)
        {
            this.writer = writer;
        }

        public void Render(IEnumerable<Bucket> summarizedForecast)
        {
            var table = new Table("Forecast", "Likelihood");
            table.Config = GetTableConfig();
            foreach (var bucket in summarizedForecast.OrderByDescending(b => b.Likelihood))
            {
                table.AddRow(bucket.Forecast, bucket.Likelihood);
            }
            writer.Write(table.ToStringWithoutExcessiveWhitespace());
        }

        protected abstract TableConfiguration GetTableConfig();
    }
}
