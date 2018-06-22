﻿using CommandLine;
using Forecaster.Application.Input;
using Forecaster.Application.Output;
using Forecaster.Core.Action;
using Forecaster.Core.Model.Action;
using Forecaster.Core.Model.Summary;
using NSubstitute;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Forecaster.Tests.Application
{
    public class ProgramShould
    {
        private readonly TextWriter stdOut = Substitute.For<TextWriter>();
        private readonly ParserAdapter parser = Substitute.For<ParserAdapter>();
        private readonly IForecastAction forecastAction = Substitute.For<IForecastAction>();
        private readonly RendererFactory rendererFactory = Substitute.For<RendererFactory>(Substitute.For<TextWriter>());
        private readonly IRenderer renderer = Substitute.For<IRenderer>();

        [Fact]
        public void Accept_Valid_Arguments_And_Print_A_Forecast()
        {
            var args = "--samples 1 2 3 4 5 --forecast 3 --trials 1000".Split(' ');
            var parsedArguments = new Options
            {
                Samples = new[] {1.0, 2.0, 3.0, 4.0, 5.0},
                Trials = 1000,
                Forecast = 3
            };
            var forecast = new Bucket[] { new Bucket(100, 1) };

            parser.Parse(args).Returns(new Alternative<Options, string>(parsedArguments));
            forecastAction.Execute(parsedArguments).Returns(forecast);
            rendererFactory.CreateFor(parsedArguments.Output).Returns(renderer);
            
            var program = new Program(parser, stdOut, forecastAction, rendererFactory);
            program.Run(args);

            parser.Received().Parse(args);
            forecastAction.Received().Execute(parsedArguments);
            rendererFactory.Received().CreateFor(parsedArguments.Output);
            renderer.Received().Render(forecast);
        }

        [Fact]
        public void Prints_Some_Help_Text_When_Arguments_Are_Invalid()
        {
            var args = "well this is clearly nonsense".Split(' ');
            var helpText = "you are doing it wrong.";
            parser.Parse(args).Returns(new Alternative<Options, string>(helpText));

            var program = new Program(parser, stdOut, forecastAction, rendererFactory);
            program.Run(args);

            forecastAction.DidNotReceive().Execute(Arg.Any<IForecastArguments>());
            rendererFactory.DidNotReceive().CreateFor(Arg.Any<OutputFormat>());
            stdOut.Received().Write(helpText);

        }
    }
}
