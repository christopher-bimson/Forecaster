using FluentAssertions;
using Forecaster.Core.Actions;
using Forecaster.Core.Model;
using NSubstitute;
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
            TrialsMock.Generate(arguments).Returns(fakeTrials);
            TrialsMock.Summarize(fakeTrials).Returns(fakeForecast);

            var action = new ForecastAction();
            var forecast = action.Execute(arguments);

            TrialsMock.Received().Generate(arguments);
            TrialsMock.Received().Summarize(fakeTrials);
            forecast.Should().Be(fakeForecast);
        }
    }
}
