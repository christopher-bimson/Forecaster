using Forecaster.Core.Model.Action;

namespace Forecaster.Core.Model.Trial
{
    public interface ITrialGenerator
    {
        Trials GenerateFrom(IForecastArguments arguments);
    }
}
