using System;
using System.Collections.Generic;
using System.Linq;

namespace Forecaster.Core.Model.Summary
{
    public class ForecastSummarizer : IForecastSummarizer
    {
        public IEnumerable<Bucket> Summarize(double[] trials)
        {
            double trialsMin = trials.Min();
            double trialsMax = trials.Max();
            int trialCount = trials.Length;

            var bucketCount = GetNumberOfBuckets(trials);
            int bucketSize = GetSizeOfBuckets(trialsMin, trialsMax, bucketCount);

            var buckets = new List<Bucket>();
            var bucketValue = 0;
            do
            {
                bucketValue += bucketSize;
                var relevantTrials = GetCountOfTrialsLargerThanBucket(trials, bucketValue);
                if (relevantTrials > 0)
                    yield return new Bucket(CalculateLikelihood(trialCount, relevantTrials), bucketValue);
                else
                    yield break;
            } while (bucketValue <= trialsMax);
        }

        private static int GetCountOfTrialsLargerThanBucket(double[] trials, int bucketValue)
        {
            return trials.Where(t => t >= bucketValue).Count();
        }

        private static decimal CalculateLikelihood(int totalTrials, int relevantTrials)
        {
            return Math.Round((relevantTrials / (decimal)totalTrials) * 100, 2);
        }

        private static bool ThereAreStillBucketsToSummarize(double biggestTrial, int bucketValue)
        {
            return bucketValue <= biggestTrial;
        }

        private static int GetSizeOfBuckets(double min, double max, int bucketCount)
        {
            return Convert.ToInt32((max - min) / bucketCount);
        }

        private static int GetNumberOfBuckets(double[] trials)
        {
            return Math.Min(trials.Distinct().Count() - 1, 10);
        }
    }
}
