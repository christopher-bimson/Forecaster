using Forecaster.Core.Model.Action;

namespace Forecaster.Core.Model.Trial
{
    public interface ITrials
    {
        double[] GenerateFrom(IForecastArguments arguments);
    }
}
