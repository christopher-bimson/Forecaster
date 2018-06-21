using System.Collections.Generic;

namespace Forecaster.Core.Model.Summary
{
    public interface IForecastSummarizer
    {
        IEnumerable<Bucket> Summarize(double[] trials);
    }
}
