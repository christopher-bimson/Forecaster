using FluentAssertions;
using Forecaster.Core.Model;
using Forecaster.Core.Model.Trial;
using Forecaster.Tests.Core.Action;
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
    }   
}
