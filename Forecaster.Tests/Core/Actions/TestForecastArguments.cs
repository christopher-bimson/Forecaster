using Forecaster.Core.Model;

namespace Forecaster.Tests.Core.Actions
{
    internal class TestForecastArguments : IForecastArguments
    {
        public TestForecastArguments(double[] samples, int forecast, int trials)
        {
            Samples = samples;
            Forecast = forecast;
            Trials = trials;
        }

        public double[] Samples { get; private set; }

        public int Forecast { get; private set; }

        public int Trials { get; private set; }
    }
}