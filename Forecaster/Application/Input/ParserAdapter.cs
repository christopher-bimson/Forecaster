using CommandLine;
using Forecaster.Core.Model.Action;
using System.Collections.Generic;

namespace Forecaster.Application.Input
{
    public class ParserAdapter
    {
        public virtual Alternative<Options, IEnumerable<Error>> Parse(string[] args)
        {
            Alternative<Options, IEnumerable<Error>> result = null;

            var parser = new Parser(with => with.CaseInsensitiveEnumValues = true);
            parser.ParseArguments<Options>(args)
                .WithParsed(options => result = new Alternative<Options, IEnumerable<Error>>(options))
                .WithNotParsed(errors => result = new Alternative<Options, IEnumerable<Error>>(errors));
            return result;
        }
    }
}
