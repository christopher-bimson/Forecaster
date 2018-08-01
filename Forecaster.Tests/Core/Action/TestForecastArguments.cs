using Forecaster.Core.Action;

namespace Forecaster.Tests.Core.Action
{
    internal class TestForecastArguments : IForecastArguments
    {
        public TestForecastArguments(double[] samples, int forecast, int trials)
        {
            Samples = samples;
            Forecast = forecast;
            TrialCount = trials;
        }

        public double[] Samples { get; private set; }

        public int Forecast { get; private set; }

        public int TrialCount { get; private set; }
    }
}