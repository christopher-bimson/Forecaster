using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecaster.Domain
{
    class CsvOutputWriter : IOutputWriter
    {
        private readonly TextWriter _textWriter;

        internal CsvOutputWriter(TextWriter textWriter)
        {
            _textWriter = textWriter ?? throw new ArgumentNullException(nameof(textWriter));
        }

        public void Write(IEnumerable<BandLikelihood> summary)
        {
            var outputBuilder = new StringBuilder();
            outputBuilder.AppendLine("Forecast Throughput,Likelihood");
            foreach(var band in summary)
            {
                outputBuilder.AppendLine(string.Join(",", $"At Least {band.Band} WUs", band.Likelihood.ToString("P3")));
            }
            _textWriter.Write(outputBuilder.ToString());
        }
    }
}
