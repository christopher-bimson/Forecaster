using CommandLine;
using Forecaster.Core.Model.Action;
using System.Collections.Generic;
using System.Linq;

namespace Forecaster.Application.Input
{
    public class Options : IForecastArguments
    {
        internal static class Help
        {
            internal const string Samples = "Sample data points of the value you want to forecast.";
            internal const string Forecast = "The number of data points 'ahead' to forecast.";
            internal const string Trials = "The number of trials to use to build the forecast. Each trial is a 'potential future' that will contribute to the forecast.";
            internal const string Output = "The output format of the forecast. Valid values are: Pretty, Markdown and JSON."; 
        }

        [Option('s', "samples", Required = true, Min = 1, HelpText = Help.Samples)]
        public IEnumerable<double> Samples { get; set; }

        [Option('f', "forecast", Required = true, HelpText = Help.Forecast)]
        public int Forecast { get; set; }

        [Option('t', "trials", Required = false, Default = 100000, HelpText = Help.Trials)]
        public int Trials { get; set; }

        [Option('o', "outputFormat", Required = false, Default = OutputFormat.Pretty, HelpText = Help.Output)]
        public OutputFormat Output { get; set; }

        double[] IForecastArguments.Samples => Samples.ToArray();

        int IForecastArguments.Forecast => Forecast;

        int IForecastArguments.TrialCount => Trials;
    }
}
