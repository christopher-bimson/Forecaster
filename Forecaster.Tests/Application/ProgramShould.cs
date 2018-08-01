using Forecaster.Application.Input;
using Forecaster.Application.Output;
using Forecaster.Core.Action;
using Forecaster.Core.Model.Summary;
using Forecaster.Core.Model.Trial;
using NSubstitute;
using System.IO;
using Xunit;

namespace Forecaster.Tests.Application
{
    public class ProgramShould
    {
        private readonly TextWriter stdOut;
        private readonly ParserAdapter parser;
        private readonly ForecastAction forecastAction;
        private readonly RendererFactory rendererFactory;
        private readonly IRenderer renderer;

        public ProgramShould()
        {
            stdOut = Substitute.For<TextWriter>();
            parser = Substitute.For<ParserAdapter>();
            var rng = Substitute.For<IRng>();
            var trialGenerator = Substitute.For<TrialGenerator>(rng);
            var summarizer = Substitute.For<ForecastSummarizer>();
            forecastAction = Substitute.For<ForecastAction>(trialGenerator, summarizer);
            rendererFactory = Substitute.For<RendererFactory>(stdOut);
            renderer = Substitute.For<IRenderer>();
        }

        [Fact]
        public void Accept_Valid_Arguments_And_Print_A_Forecast()
        {
            var validArguments = "--samples 1 2 3 4 5 --forecast 3 --trials 1000".Split(' ');
            var parsedValidArguments = new Options
            {
                Samples = new[] {1.0, 2.0, 3.0, 4.0, 5.0},
                Trials = 1000,
                Forecast = 3
            };
            var forecast = new[] { new Bucket(100, 1) };

            parser.Parse(validArguments).Returns(new Alternative<Options, string>(parsedValidArguments));
            forecastAction.Execute(parsedValidArguments).Returns(forecast);
            rendererFactory.CreateFor(parsedValidArguments.Output).Returns(renderer);
            
            var program = new Program(parser, stdOut, forecastAction, rendererFactory);
            program.Run(validArguments);

            parser.Received().Parse(validArguments);
            forecastAction.Received().Execute(parsedValidArguments);
            rendererFactory.Received().CreateFor(parsedValidArguments.Output);
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
