using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Forecaster.Core.Model.Summary;

namespace Forecaster.Application.Output
{
    public class CsvRenderer : IRenderer
    {
        private TextWriter writer;

        public CsvRenderer(TextWriter writer)
        {
            this.writer = writer;
        }

        public void Render(IEnumerable<Bucket> summary)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Forecast,Likelihood");
            foreach (var bucket in summary)
            {
                builder.AppendFormat("{0},{1}{2}", bucket.Forecast, bucket.Likelihood, 
                    Environment.NewLine);
            }
            writer.Write(builder.ToString());
        }
    }
}
