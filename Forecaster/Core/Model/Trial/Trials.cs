using Forecaster.Core.Model.Action;
using System;
using System.Linq;

namespace Forecaster.Core.Model.Trial
{
    public class Trials : ITrials
    {
        private readonly IRng rng;

        public Trials(IRng rng)
        {
            this.rng = rng;
        }

        public double[] GenerateFrom(IForecastArguments arguments)
        {
            var result = new double[arguments.TrialCount];
            for(int i = 0; i < result.Length; i++)
            {
                result[i] = arguments.Samples
                    .SampleWithReplacement(arguments.Forecast, rng)
                    .Sum();
            }
            return result;
        }
    }
}
