using FluentAssertions;
using Forecaster.Core.Model.Summary;
using System.Linq;
using Xunit;

namespace Forecaster.Tests.Core.Model.Summary
{
    public class ForecastSummarizerShould
    {

        [Fact]
        public void Summarize_10_Trials_Incrementing_By_10_Into_10_Bands()
        {
            var trialData = new double[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
            var expected = new[] {
                new Bucket(10, 100),
                new Bucket(20, 90),
                new Bucket(30, 80),
                new Bucket(40, 70),
                new Bucket(50, 60),
                new Bucket(60, 50),
                new Bucket(70, 40),
                new Bucket(80, 30),
                new Bucket(90, 20),
                new Bucket(100, 10),
            };

            var summarizer = new ForecastSummarizer();
            var summary = summarizer.Summarize(trialData);

            summary.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Summarize_5_Trials_Incrementing_By_10_Into_5_Bands()
        {
            var trialData = new double[] { 20, 40, 60, 80, 100 };
            var expected = new[] {
                new Bucket(20, 100),
                new Bucket(40, 80),
                new Bucket(60, 60),
                new Bucket(80, 40),
                new Bucket(100, 20),
            };

            var summarizer = new ForecastSummarizer();
            var summary = summarizer.Summarize(trialData);

            summary.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Summarize_Large_Number_Of_Repeated_Trials_Into_Bands()
        {
            var list = Enumerable.Range(1, 5000).ToList();
            list.AddRange(Enumerable.Range(1, 5000));
            var expected = new[] {
                new Bucket(90.02m, 500),
                new Bucket(80.02m, 1000),
                new Bucket(70.02m, 1500),
                new Bucket(60.02m, 2000),
                new Bucket(50.02m, 2500),
                new Bucket(40.02m, 3000),
                new Bucket(30.02m, 3500),
                new Bucket(20.02m, 4000),
                new Bucket(10.02m, 4500),
                new Bucket(0.02m, 5000),
            };

            var summarizer = new ForecastSummarizer();
            var summary = summarizer.Summarize(list.ConvertAll(x => (double)x).ToArray());

            summary.Should().BeEquivalentTo(expected);
        }

        /// <remarks>
        /// https://christopher-bimson.github.io/blog/2017/04/19/forecaster
        /// </remarks>
        [Fact]
        public void Reasonably_Approximate_The_Worked_Example_From_The_Forecaster_Blog()
        {
            var data = new double[] { 76, 59, 61, 49, 60 };
            var expected = new[]
            {
                new Bucket(100m, 49),
                new Bucket(80m, 56),
                new Bucket(20m, 70),
            };

            var summarizer = new ForecastSummarizer();
            var summary = summarizer.Summarize(data);

            summary.Should().BeEquivalentTo(expected);
        }
    }
}
