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
            var bucketCount = GetBucketCount(trials);
            var bucketSize = GetBucketSize(trials, bucketCount);

            var bucketValue = bucketSize;
            while (bucketValue <= trials.Max())
            {
                var matchingTrials = trials.Where(t => t >= bucketValue).Count();
                if (matchingTrials > 0)
                {
                    results.Add(new Bucket(CalculateLikelihood(matchingTrials, matchingTrials), bucketValue));
                }
                bucketValue += bucketSize;
            }
            return results;
        }

        private static double CalculateLikelihood(int matchingTrials, int totalTrials)
        {
            return (matchingTrials / (double)totalTrials) * 100;
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
