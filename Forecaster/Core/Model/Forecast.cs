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
            int bucketCount = GetForecaseBucketCount(trials);
            int bucketSize = GetForecastBucketSize(trials, bucketCount);
            double biggestTrial = trials.Max();

            var bucketValue = bucketSize;
            while (bucketValue <= biggestTrial)
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

        private static int GetForecastBucketSize(double[] trials, int bucketCount)
        {
            return Convert.ToInt32((trials.Max() - trials.Min()) / bucketCount);
        }

        private static int GetForecaseBucketCount(double[] trials)
        {
            return Math.Min(trials.Distinct().Count() - 1, 10);
        }
    }
}
