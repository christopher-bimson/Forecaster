using System;
using System.Collections.Generic;
using System.Text;

namespace Forecaster.Core.Model
{
    public interface ITrials
    {
        double[] GenerateFor(IForecastArguments arguments);
        IEnumerable<Band> Summarize(double[] trials);
    }
}
