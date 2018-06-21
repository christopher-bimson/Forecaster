﻿using Forecaster.Application;
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
            var forecastAction = new ForecastAction(new Trials(new RandomRng()),
                new ForecastSummarizer());

            var program = new Program(new ParserAdapter(), forecastAction, new RendererFactory());
            program.Run(args);
        }

        private readonly ParserAdapter parser;
        private readonly IForecastAction forecastAction;
        private readonly RendererFactory rendererFactory;

        public Program(ParserAdapter parser, IForecastAction forecastAction, RendererFactory rendererFactory)
        {
            this.parser = parser;
            this.forecastAction = forecastAction;
            this.rendererFactory = rendererFactory;
        }

        public void Run(string[] args)
        {
            var parserResult = parser.Parse(args);
            if (parserResult.IsSuccess)
            {
                var summarizedForecast = forecastAction.Execute(parserResult.Success);
                var renderer = rendererFactory.Create();
                renderer.Render(summarizedForecast);
            }
        }
    }
}
