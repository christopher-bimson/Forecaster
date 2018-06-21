using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forecaster.Application
{
    public class Options
    {
        [Option('s', "samples", Required = true, Min = 1)]
        public IEnumerable<double> Samples { get; set; }

        [Option('f', "forecast", Required = true)]
        public int Forecast { get; set; }

        [Option('t', "trials", Required = false, Default = 10000)]
        public int Trials { get; set; }
    }
}
