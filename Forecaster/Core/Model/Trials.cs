using System;
using System.Collections.Generic;
using System.Text;

namespace Forecaster.Core.Model
{
    public class Trials : ITrials
    {
        private readonly IRng rng;

        public Trials(IRng rng)
        {
            this.rng = rng;
        }

        public double[] GenerateFor(IForecastArguments arguments)
        {
            var result = new List<double>();
            for(int i = 0; i < arguments.TrialCount; i++)
            {
                double sum = 0;
                for(int j = 0; j < arguments.Forecast; j++)
                {
                    sum += arguments.Samples[rng.Next(arguments.Samples.Length)];
                }
                result.Add(sum);
            }
            return result.ToArray();
        }

        public IEnumerable<Band> Summarize(double[] trials)
        {
            throw new NotImplementedException();
        }
    }
}
