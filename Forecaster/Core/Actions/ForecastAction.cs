using Forecaster.Core.Model;
using System;
using System.Collections.Generic;

namespace Forecaster.Core.Actions
{
    public class ForecastAction
    {
        private readonly ITrials trials;
        private readonly IForecast forecast;

        public ForecastAction(ITrials trials, IForecast forecast)
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
