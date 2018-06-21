using System;
using System.Collections.Generic;
using System.Text;

namespace Forecaster.Core.Model
{
    public interface IForecastArguments
    {
        double[] Samples { get; }
        int Forecast { get; }
        int TrialCount { get; }
    }
}
