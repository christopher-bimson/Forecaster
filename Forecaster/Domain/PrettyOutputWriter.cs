using System.Collections.Generic;
using ConsoleTables;
using System.Linq;

namespace Forecaster.Domain
{
    internal class PrettyOutputWriter : IOutputWriter
    {
        public void Write(IEnumerable<BandLikelihood> summary)
        {
            var table = ConsoleTable.From(summary.Select(o => new PrettyBandLikelihoodAdapter(o)));
            table.Options.EnableCount = false;
            table.Columns = new[] { "Forecast Throughput", "Likelihood" };
            table.Write();
        }

        internal class PrettyBandLikelihoodAdapter
        {
            private readonly BandLikelihood _bandLikelyhood;

            internal PrettyBandLikelihoodAdapter(BandLikelihood bandLikelyhood)
            {
                _bandLikelyhood = bandLikelyhood;
            }

            public string ForecastThroughput
            {
                get
                {
                    return $"At Least {_bandLikelyhood.Band} WUs";
                }
            }

            public string Likelyhood
            {
                get
                {
                    return _bandLikelyhood.Likelihood.ToString("P3");
                }
            }
        }
    }
}
