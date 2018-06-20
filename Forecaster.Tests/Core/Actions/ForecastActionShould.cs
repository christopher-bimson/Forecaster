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
        private IForecast ForecastMock = Substitute.For<IForecast>();


        [Fact]
        public void Generate_A_Forecast_From_Valid_Arguments()
        {
            IForecastArguments arguments = new TestForecastArguments(new[] { 5.0, 5.0, 5.0 }, 5, 1000);
            var fakeTrials = new double[] { 1, 2, 3, 4, 5 };
            var fakeForecast = new Bucket[] { new Bucket(100.0, 1) };

            TrialsMock.GenerateFrom(arguments).Returns(fakeTrials);
            ForecastMock.Summarize(fakeTrials).Returns(fakeForecast);

            var action = new ForecastAction(TrialsMock, ForecastMock);
            var forecast = action.Execute(arguments);

            TrialsMock.Received().GenerateFrom(arguments);
            ForecastMock.Received().Summarize(fakeTrials);
            forecast.Should().BeSameAs(fakeForecast);
        }

        [Fact]
        public void Throw_If_Asked_To_Generate_A_Forecast_From_Null_Arguments()
        {
            var action = new ForecastAction(TrialsMock, ForecastMock);

            Action generate = () => action.Execute(null);

            generate.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Throw_On_Construction_If_ITrial_Invariant_Is_Not_Satisfied()
        {
            Action constructor = () => new ForecastAction(null, ForecastMock);

            constructor.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Throw_On_Construction_If_IForecast_Invariant_Is_Not_Satisfied()
        {
            Action constructor = () => new ForecastAction(TrialsMock, null);

            constructor.Should().Throw<ArgumentNullException>();
        }
    }
}
