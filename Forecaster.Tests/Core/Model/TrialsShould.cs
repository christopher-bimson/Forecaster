using FluentAssertions;
using Forecaster.Core.Model;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Forecaster.Tests.Core.Model
{
    public class TrialsShould
    {
        [Fact]
        public void Generate_A_Set_Of_Trials_From_Valid_Arguments()
        {
            var rng = new RepeatingRng(new[] { 0, 1, 2, 3, 4 });
            var arguments = new TestForecastArguments(new[] { 1.0, 2.0, 3.0, 4.0, 5.0 },
                5, 10);
            var expectedTrialValue = arguments.Samples.Sum();

            var trials = new Trials(rng);
            var trialData = trials.GenerateFrom(arguments);

            trialData.Length.Should().Be(arguments.TrialCount);
            trialData.ToList().ForEach(value => value.Should().Be(expectedTrialValue));
        }

        [Fact]
        public void Summarize_10_Trials_Incrementing_By_10_Into_10_Bands()
        {
            var trialData = new double[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
            var expectedBands = new [] {
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

            var trials = new Trials(Substitute.For<IRng>());
            var bands = trials.Summarize(trialData);

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

            var trials = new Trials(Substitute.For<IRng>());
            var bands = trials.Summarize(trialData);

            bands.Should().BeEquivalentTo(expectedBands);
        }

        [Fact]
        public void Summarize_Large_Number_Of_Repeated_Trials_Into_Bands()
        {
            var list = Enumerable.Range(1, 5000).ToList();
            list.AddRange(Enumerable.Range(1, 5000));


            var trials = new Trials(Substitute.For<IRng>());
            var bands = trials.Summarize(list.ConvertAll<double>(x => (double)x).ToArray());

            //bands.Should().BeEquivalentTo(expectedBands);
        }
    }
}
