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
            var threshold = GetStartingThreshold(trialsMin, bucketSize);
            do
            {
                threshold += bucketSize;
                var likelihood = CalculateLikelihood(trialCount, TrialsThatMeetThreshold(trials, threshold));
                if (likelihood > 0)
                    yield return new Bucket(likelihood, threshold);
            } while (ThereAreStillBucketsToSummarize(trialsMax, threshold));
        }

        private static int GetStartingThreshold(double trialsMin, int bucketSize)
        {
            return Math.Max(0, Convert.ToInt32(trialsMin - bucketSize));
        }

        private static int TrialsThatMeetThreshold(double[] trials, int bucketThreshold)
        {
            return trials.Where(t => t >= bucketThreshold).Count();
        }

        private static decimal CalculateLikelihood(int totalTrials, int relevantTrials)
        {
            return Math.Round((relevantTrials / (decimal)totalTrials) * 100, 2);
        }

        private static bool ThereAreStillBucketsToSummarize(double biggestTrial, int bucketThreshold)
        {
            return bucketThreshold <= biggestTrial;
        }

        private static int GetSizeOfBuckets(double min, double max, int bucketCount)
        {
            var diff = max - min;

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
