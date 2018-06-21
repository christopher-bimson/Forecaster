using System;
using System.Collections.Generic;
using System.Text;

namespace Forecaster.Core.Model
{
    public interface IForecast
    {
        IEnumerable<Bucket> Summarize(double[] trials);
    }
}
