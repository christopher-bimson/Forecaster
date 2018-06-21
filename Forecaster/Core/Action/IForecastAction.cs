using System.Collections.Generic;
using Forecaster.Core.Model.Action;
using Forecaster.Core.Model.Summary;

namespace Forecaster.Core.Action
{
    public interface IForecastAction
    {
        IEnumerable<Bucket> Execute(IForecastArguments arguments);
    }
}