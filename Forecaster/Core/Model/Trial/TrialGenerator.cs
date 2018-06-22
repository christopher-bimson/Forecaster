using Forecaster.Core.Model.Action;
using System;
using System.Linq;

namespace Forecaster.Core.Model.Trial
{
    public class TrialGenerator : ITrialGenerator
    {
        private readonly IRng rng;

        public TrialGenerator(IRng rng)
        {
            this.rng = rng;
        }

        public Trials GenerateFrom(IForecastArguments arguments)
        {
            var result = new double[arguments.TrialCount];
            for(int i = 0; i < result.Length; i++)
            {
                result[i] = arguments.Samples
                    .PickWithReplacement(arguments.Forecast, rng)
                    .Sum();
            }
            return result;
        }
    }
}
