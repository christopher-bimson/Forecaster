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
            var buckets = new List<Bucket>();
            int bucketCount = GetBucketCount(trials);
            int bucketSize = GetBucketSize(trials, bucketCount);

            var bucketValue = bucketSize;
            while (bucketValue <= trials.Max())
            {
                var trialCount = trials.Where(t => t >= bucketValue).Count();
                if (trialCount > 0)
                {
                    buckets.Add(new Bucket((trialCount / (double)trials.Length) * 100, bucketValue));
                }
                bucketValue += bucketSize;
            }
            return buckets;
        }

        private static int GetBucketSize(double[] trials, int bucketCount)
        {
            return Convert.ToInt32((trials.Max() - trials.Min()) / bucketCount);
        }

        private static int GetBucketCount(double[] trials)
        {
            return Math.Min(trials.Distinct().Count() - 1, 10);
        }
    }
}
