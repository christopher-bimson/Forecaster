using Forecaster.Core.Model;
using System;
using System.Collections.Generic;

namespace Forecaster.Core.Actions
{
    public class ForecastAction
    {
        private readonly ITrials trials;

        public ForecastAction(ITrials trials)
        {
            if (trials == null)
                throw new ArgumentNullException(nameof(trials));

            this.trials = trials;
        }

        public IEnumerable<Bucket> Execute(IForecastArguments arguments)
        {
            if (arguments == null)
                throw new ArgumentNullException(nameof(arguments));

            var data = trials.GenerateFrom(arguments);
            return trials.Summarize(data);
        }
    }
}
