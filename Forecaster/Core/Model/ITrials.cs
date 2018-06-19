using System;
using System.Collections.Generic;
using System.Text;

namespace Forecaster.Core.Model
{
    public interface ITrials
    {
        double[] GenerateFrom(IForecastArguments arguments);
        IEnumerable<Bucket> Summarize(double[] trials);
    }
}
