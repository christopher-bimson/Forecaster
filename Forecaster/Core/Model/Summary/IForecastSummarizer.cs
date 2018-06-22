using Forecaster.Core.Model.Trial;
using System.Collections.Generic;

namespace Forecaster.Core.Model.Summary
{
    public interface IForecastSummarizer
    {
        IEnumerable<Bucket> Summarize(Trials trials);
    }
}
