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

        public IEnumerable<Band> Execute(IForecastArguments arguments)
        {
            if (arguments == null)
                throw new ArgumentNullException(nameof(arguments));

            var data = trials.GenerateFor(arguments);
            return trials.Summarize(data);
        }
    }
}
