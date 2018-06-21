using CommandLine;
using Forecaster.Core.Model.Action;
using System.Collections.Generic;
using System.Linq;

namespace Forecaster.Application.Input
{
    public class Options : IForecastArguments
    {
        [Option('s', "samples", Required = true, Min = 1)]
        public IEnumerable<double> Samples { get; set; }

        [Option('f', "forecast", Required = true)]
        public int Forecast { get; set; }

        [Option('t', "trials", Required = false, Default = 10000)]
        public int Trials { get; set; }

        [Option('o', "outputFormat", Required = false, Default = OutputFormat.Pretty)]
        public OutputFormat Output { get; set; }

        double[] IForecastArguments.Samples => Samples.ToArray();

        int IForecastArguments.Forecast => Forecast;

        int IForecastArguments.TrialCount => Trials;
    }
}
