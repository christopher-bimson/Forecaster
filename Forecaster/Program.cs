using Forecaster.Application.Input;
using Forecaster.Application.Output;
using Forecaster.Core.Action;
using Forecaster.Core.Model.Summary;
using Forecaster.Core.Model.Trial;
using System;
using System.IO;

namespace Forecaster
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var forecastAction = new ForecastAction(new TrialGenerator(new SystemRandomRng()),
                new ForecastSummarizer());

            var program = new Program(new ParserAdapter(), Console.Out, forecastAction, 
                new RendererFactory(Console.Out));

            program.Run(args);
        }

        private readonly ParserAdapter parser;
        private readonly ForecastAction forecastAction;
        private readonly RendererFactory rendererFactory;
        private readonly TextWriter stdOut;

        public Program(ParserAdapter parser, TextWriter stdOut,  ForecastAction forecastAction, 
            RendererFactory rendererFactory)
        {
            this.parser = parser;
            this.forecastAction = forecastAction;
            this.rendererFactory = rendererFactory;
            this.stdOut = stdOut;
        }

        public void Run(string[] args)
        {
            var parserResult = parser.Parse(args);
            if (parserResult.IsSuccess)
            {
                var summarizedForecast = forecastAction.Execute(parserResult.SuccessResult);
                var renderer = rendererFactory.CreateFor(parserResult.SuccessResult.Output);
                renderer.Render(summarizedForecast);
            }
            else
            {
                stdOut.Write(parserResult.FailResult);
            }
        }
    }
}
