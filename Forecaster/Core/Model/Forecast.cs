using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forecaster.Core.Model
{
    public class Forecast : IForecast
    {
        public IEnumerable<Bucket> Summarize(double[] trials)
        {
            var buckets = new List<Bucket>();
            int bucketCount = GetForecastBucketCount(trials);
            int bucketSize = GetForecastBucketSize(trials, bucketCount);
            double biggestTrial = trials.Max();

            var bucketValue = bucketSize;
            while (ThereAreStillBucketsToSummarize(biggestTrial, bucketValue))
            {
                CalculateLikelihoodForTrialsInBucket(trials, buckets, bucketValue);
                bucketValue += bucketSize;
            }
            return buckets;
        }

        private static void CalculateLikelihoodForTrialsInBucket(double[] trials, List<Bucket> buckets, int bucketValue)
        {
            var trialCount = trials.Where(t => t >= bucketValue).Count();
            if (trialCount > 0)
            {
                buckets.Add(new Bucket(Math.Round((trialCount / (decimal)trials.Length) * 100, 2), bucketValue));
            }
        }

        private static bool ThereAreStillBucketsToSummarize(double biggestTrial, int bucketValue)
        {
            return bucketValue <= biggestTrial;
        }

        private static int GetForecastBucketSize(double[] trials, int bucketCount)
        {
            return Convert.ToInt32((trials.Max() - trials.Min()) / bucketCount);
        }

        private static int GetForecastBucketCount(double[] trials)
        {
            return Math.Min(trials.Distinct().Count() - 1, 10);
        }
    }
}
