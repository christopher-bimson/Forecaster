using Forecaster.Core.Model.Action;
using Forecaster.Core.Model.Summary;
using Forecaster.Core.Model.Trial;
using System;
using System.Collections.Generic;

namespace Forecaster.Core.Action
{
    public class ForecastAction : IForecastAction
    {
        private readonly ITrials trials;
        private readonly IForecastSummarizer forecast;

        public ForecastAction(ITrials trials, IForecastSummarizer forecast)
        {
            if (trials == null)
                throw new ArgumentNullException(nameof(trials));

            if (forecast == null)
                throw new ArgumentNullException(nameof(forecast));

            this.trials = trials;
            this.forecast = forecast;
        }

        public IEnumerable<Bucket> Execute(IForecastArguments arguments)
        {
            if (arguments == null)
                throw new ArgumentNullException(nameof(arguments));

            var data = trials.GenerateFrom(arguments);
            return forecast.Summarize(data);
        }
    }
}
