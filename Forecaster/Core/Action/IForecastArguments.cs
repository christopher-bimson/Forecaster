namespace Forecaster.Core.Action
{
    public interface IForecastArguments
    {
        double[] Samples { get; }
        int Forecast { get; }
        int TrialCount { get; }
    }
}
