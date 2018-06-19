using FluentAssertions;
using Forecaster.Core.Model;
using NSubstitute;
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
        public void Summarize_A_Set_Of_Trials_Into_10_Bands()
        {
            var trialData = new double[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
            var expectedBands = new [] {
                new Band(10, 100),
                new Band(20, 90),
                new Band(30, 80),
                new Band(40, 70),
                new Band(50, 60),
                new Band(60, 50),
                new Band(70, 40),
                new Band(80, 30),
                new Band(90, 20),
                new Band(100, 10),
            };

            var trials = new Trials(Substitute.For<IRng>());
            var bands = trials.Summarize(trialData);

            bands.Should().BeEquivalentTo(expectedBands);
        }
    }
}
