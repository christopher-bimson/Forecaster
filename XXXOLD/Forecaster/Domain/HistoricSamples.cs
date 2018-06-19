using System;
using System.Collections.Generic;
using System.Linq;

namespace Forecaster.Domain
{
    public class HistoricSamples
    {
        private readonly Random _rng;
        private readonly double[] _historicSamples;

        public HistoricSamples(Random rng, IEnumerable<double> historicSamples)
        {
            if (rng == null)
                throw new ArgumentNullException(nameof(rng));

            if (historicSamples == null)
                throw new ArgumentNullException(nameof(historicSamples));

            if (!historicSamples.Any())
                throw new ArgumentException("At least one historic data point is required.", 
                    nameof(historicSamples));

            _rng = rng;
            _historicSamples = historicSamples.ToArray();
        }

        public double Next()
        {
            return _historicSamples[_rng.Next(_historicSamples.Length)];
        }

        public double[] Next(int count)
        {
            var result = new double[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = Next();
            }
            return result;
        }
    }
}