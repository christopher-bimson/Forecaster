using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Forecaster.Domain
{
    public static class Trials
    {
        public static Trial[] Generate(HistoricSamples historicSamples, int timeUnitsToForecast, int numberOfTrials)
        {
            if (historicSamples == null)
                throw new ArgumentNullException(nameof(historicSamples));

            if (timeUnitsToForecast < 0)
                throw new ArgumentException("You can't forecast the past.", nameof(timeUnitsToForecast));

            if (numberOfTrials < 0)
                throw new ArgumentException("You have to run at least one trial.", nameof(numberOfTrials));

            var trials = new Trial[numberOfTrials];
            for (var i = 0; i < numberOfTrials; i++)
            {
                trials[i] = new Trial(historicSamples, timeUnitsToForecast);
            }
            return trials;
        }

        public static void Summarize(this Trial[] trials, IOutputWriter writer, int summaryBandSize = 10)
        {
            if (trials == null)
                throw new ArgumentNullException(nameof(trials));

            if (!trials.Any())
                throw new ArgumentException("There must be at least 1 trial.");

            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            if (summaryBandSize < 1)
                throw new ArgumentException("Trials must be summarized into bands of at least 1 WU.",
                    nameof(summaryBandSize));

            var bands = CalculateSummarizedBands(trials, summaryBandSize);
            var summary = SummarizeTrials(trials, bands);
            writer.Write(summary);
        }

        private static IEnumerable<int> CalculateSummarizedBands(IEnumerable<Trial> trials, int bandSize)
        {
            return trials.Select(t => t.Total.FloorToNearest(bandSize)).Distinct().OrderBy(i => i);
        }

        private static IEnumerable<BandLikelihood> SummarizeTrials(Trial[] trials, IEnumerable<int> bands)
        {
            var summary = new List<BandLikelihood>();
            foreach (var threshold in bands)
            {
                summary.Add(new BandLikelihood(threshold, CalculateLikelihoodForBand(trials, threshold)));
            }
            return summary;
        }

        private static double CalculateLikelihoodForBand(Trial[] trials, int band)
        {
            return (double)trials.Count(trial => trial.Total >= band) / trials.Length;
        }
    }
}