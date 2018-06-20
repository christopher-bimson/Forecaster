using FluentAssertions;
using Forecaster.Core.Model;
using System.Linq;
using Xunit;

namespace Forecaster.Tests.Core.Model
{
    public class ForecastShould
    {

        [Fact]
        public void Summarize_10_Trials_Incrementing_By_10_Into_10_Bands()
        {
            var trialData = new double[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
            var expectedBands = new[] {
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


            var forecast = new Forecast();
            var bands = forecast.Summarize(trialData);

            bands.Should().BeEquivalentTo(expectedBands);
        }

        [Fact]
        public void Summarize_5_Trials_Incrementing_By_10_Into_10_Bands()
        {
            var trialData = new double[] { 20, 40, 60, 80, 100 };
            var expectedBands = new[] {
                new Bucket(20, 100),
                new Bucket(40, 80),
                new Bucket(60, 60),
                new Bucket(80, 40),
                new Bucket(100, 20),
            };

            var forecast = new Forecast();
            var bands = forecast.Summarize(trialData);

            bands.Should().BeEquivalentTo(expectedBands);
        }

        [Fact]
        public void Summarize_Large_Number_Of_Repeated_Trials_Into_Bands()
        {
            var list = Enumerable.Range(1, 5000).ToList();
            list.AddRange(Enumerable.Range(1, 5000));
            var expectedBands = new[] {
                new Bucket(90.02, 500),
                new Bucket(80.02, 1000),
                new Bucket(70.02, 1500),
                new Bucket(60.02, 2000),
                new Bucket(50.02, 2500),
                new Bucket(40.02, 3000),
                new Bucket(30.02, 3500),
                new Bucket(20.02, 4000),
                new Bucket(10.02, 4500),
                new Bucket(0.02, 5000),
            };

            var forecast = new Forecast();
            var bands = forecast.Summarize(list.ConvertAll<double>(x => (double)x).ToArray());

            bands.Should().BeEquivalentTo(expectedBands);
        }
    }
}
