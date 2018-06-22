using CommandLine;
using CommandLine.Text;
using System;

namespace Forecaster.Application.Input
{
    public class ParserAdapter
    {
        public virtual Alternative<Options, String> Parse(string[] args)
        {
            Alternative<Options, String> result = null;

            var parser = new Parser(with => with.CaseInsensitiveEnumValues = true);
            var parserResult = parser.ParseArguments<Options>(args);

            parserResult
                .WithParsed(options => result = new Alternative<Options, String>(options))
                .WithNotParsed(errors => result = new Alternative<Options, String>(HelpText.AutoBuild(parserResult).ToString()));

            return result;
        }
    }
}
