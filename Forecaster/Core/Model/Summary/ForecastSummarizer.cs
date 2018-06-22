using Forecaster.Core.Model.Trial;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Forecaster.Core.Model.Summary
{
    public class ForecastSummarizer : IForecastSummarizer
    {
        public IEnumerable<Bucket> Summarize(Trials trials)
        {
            var targetBucketCount = GetNumberOfBuckets(trials);
            var bucketSize = GetSizeOfBuckets(trials, targetBucketCount);

            var buckets = new Buckets();
            var threshold = GetStartingThreshold(trials.Min, bucketSize);
            do
            {
                threshold += bucketSize;
                var likelihood = trials.CalculateLikelihoodOf(threshold);
                buckets.Add(likelihood, threshold);
            } while (AreLikelihoodsToCalculate(trials.Max, threshold));
            return buckets;
        }

        private static int GetStartingThreshold(double trialsMin, int bucketSize)
        {
            return Math.Max(0, Convert.ToInt32(trialsMin - bucketSize));
        }

        private static bool AreLikelihoodsToCalculate(double biggestTrial, int bucketThreshold)
        {
            return bucketThreshold <= biggestTrial;
        }

        private static int GetSizeOfBuckets(Trials trials, int bucketCount)
        {
            var diff = trials.Max - trials.Min;

            if (diff == 0)
                return 1;

            return Convert.ToInt32(Math.Ceiling(diff / bucketCount));
        }

        private static int GetNumberOfBuckets(double[] trials)
        {
            return Math.Min(trials.Distinct().Count() - 1, 10);
        }
    }
}
