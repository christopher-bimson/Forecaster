using System;
using CommandLine.Text;
using Forecaster.Domain;

namespace Forecaster
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var options = new Options();
            if (!CommandLine.Parser.Default.ParseArguments(args, options))
            {
                Console.Write(HelpText.AutoBuild(options));
                Environment.Exit(-1);
            }
           
            var historicSamples = new HistoricSamples(new Random(), options.HistoricThroughput);

            var trials = Trials.Generate(historicSamples, options.PeriodsToForecast, options.NumberOfTrials);
            trials.Summarize(new JsonOutputWriter(Console.Out));
        }
    }
}
