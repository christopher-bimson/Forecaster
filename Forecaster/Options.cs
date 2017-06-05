using CommandLine;

namespace Forecaster
{
    public enum ForecastOutputFormat
    {
        Pretty,
        Json,
        Csv
    }

    public class Options
    {
        private const string HistoricThroughputHelpText =
            "An array of numeric values that represent throughput in some work unit (e.g. story points, story count, ideal hours - abbreviated WU) " +
            "/ time period (e.g. week, iteration, month).";

        private const string PeriodsToForecastText =
            "The number of time periods to forecast. Must be a positive value.";

        private const string NumberOfTrialsHelpText =
            "The number of trials (consider a trial to be a 'potential future') to use when calculating the forecast.";

        private const string OutputFormatHelpText =
            "The format to use when outputting results. Choose from Pretty, Json and CSV.";

        [OptionArray('h', "historicThroughput", Required = true, HelpText = HistoricThroughputHelpText)]
        public double[] HistoricThroughput { get; set; }

        [Option('p', "periodsToForecast", Required = true, HelpText = PeriodsToForecastText)]
        public int PeriodsToForecast { get; set; }

        [Option('t', "numberOfTrials", DefaultValue = 100000, HelpText = NumberOfTrialsHelpText)]
        public int NumberOfTrials { get; set; }

        [Option('o', "outputFormat", DefaultValue = ForecastOutputFormat.Pretty, HelpText = OutputFormatHelpText)]
        public ForecastOutputFormat OutputFormat { get; set; }


        public Options()
        {
            HistoricThroughput = new[] {0.0};
        }
    }
}
