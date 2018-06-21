using CommandLine;
using Forecaster.Core.Model.Action;
using System.Collections.Generic;

namespace Forecaster.Application
{
    public class ParserAdapter
    {
        public virtual Alternative<IForecastArguments, IEnumerable<Error>> Parse(string[] args)
        {
            Alternative<IForecastArguments, IEnumerable<Error>> result = null;
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(options => result = new Alternative<IForecastArguments, IEnumerable<Error>>(options))
                .WithNotParsed(errors => result = new Alternative<IForecastArguments, IEnumerable<Error>>(errors));
            return result;
        }
    }
}
