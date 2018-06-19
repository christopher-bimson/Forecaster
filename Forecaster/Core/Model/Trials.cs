using System;
using System.Collections.Generic;
using System.Linq;

namespace Forecaster.Core.Model
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

        public IEnumerable<Bucket> Summarize(double[] trials)
        {
            var results = new List<Bucket>();
            var BucketCount = Math.Min(trials.Distinct().Count()-1, 10);

            var bandSize = Convert.ToInt32((trials.Max() - trials.Min()) / BucketCount);

            var banding = bandSize;
            while (banding <= trials.Max())
            {
                var trialCount = trials.Where(v => v >= banding).Count();
                if (trialCount > 0)
                {
                    results.Add(new Bucket((trialCount / (double)trials.Length) * 100, banding));
                }
                banding += bandSize;
            }
            return results;
        }
    }
}
