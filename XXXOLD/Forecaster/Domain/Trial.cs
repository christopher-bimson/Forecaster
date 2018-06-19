using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Forecaster.Domain
{
    public class Trial : IEnumerable<double>
    {
        private readonly double[] _trial;

        public Trial(HistoricSamples historicSamples, int timeUnitsToForecast)
        {
            _trial = historicSamples.Next(timeUnitsToForecast);
            Total = _trial.Sum();
        }

        public IEnumerator<double> GetEnumerator()
        {
            return _trial.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public double Total { get; private set; }        
    }
}