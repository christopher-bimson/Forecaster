using FluentAssertions;
using Forecaster.Core.Actions;
using Forecaster.Core.Model;
using Forecaster.Tests.Core.Model;
using NSubstitute;
using System;
using Xunit;

namespace Forecaster.Tests.Core.Actions
{
    public class ForecastActionShould
    {
        private ITrials TrialsMock = Substitute.For<ITrials>();


        [Fact]
        public void Generate_A_Forecast_From_Valid_Arguments()
        {
            IForecastArguments arguments = new TestForecastArguments(new[] { 5.0, 5.0, 5.0 }, 
                5, 1000);
            var fakeTrials = new double[] { 1, 2, 3, 4, 5 };
            var fakeForecast = new Band[] { new Band(100.0, 1) };
            TrialsMock.GenerateFrom(arguments).Returns(fakeTrials);
            TrialsMock.Summarize(fakeTrials).Returns(fakeForecast);

            var action = new ForecastAction(TrialsMock);
            var forecast = action.Execute(arguments);

            TrialsMock.Received().GenerateFrom(arguments);
            TrialsMock.Received().Summarize(fakeTrials);
            forecast.Should().BeSameAs(fakeForecast);
        }

        [Fact]
        public void Throw_If_Asked_To_Generate_A_Forecast_From_Null_Arguments()
        {
            var action = new ForecastAction(TrialsMock);

            Action generate = () => action.Execute(null);

            generate.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Throw_On_Construction_If_Invariants_Are_Not_Satisfied()
        {
            Action constructor = () => new ForecastAction(null);

            constructor.Should().Throw<ArgumentNullException>();
        }
    }
}
