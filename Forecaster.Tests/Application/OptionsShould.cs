using CommandLine;
using FluentAssertions;
using Forecaster.Application;
using Xunit;

namespace Forecaster.Tests.Application
{
    public class OptionsShould
    {
        [Theory]
        [InlineData("--samples 1 2 3 --forecast 5 --trials 1000", 
            new[] { 1.0, 2.0, 3.0}, 5, 1000)]
        [InlineData("-s 1 2 3 -f 5 -t 1000",
            new[] { 1.0, 2.0, 3.0 }, 5, 1000)]
        [InlineData("--samples 1 2 3 -f 5",
            new[] { 1.0, 2.0, 3.0 }, 5, 10000)]
        public void Parse_If_Syntactically_Valid(string args, double[] expectedSamples, 
            int expectedForecast, int expectedTrials)
        {
            Parser.Default.ParseArguments<Options>(args.Split(' '))
                .WithParsed(options => 
                {
                    options.Samples.Should().BeEquivalentTo(expectedSamples);
                    options.Forecast.Should().Be(expectedForecast);
                    options.Trials.Should().Be(expectedTrials);
                })
                .WithNotParsed(errors =>
                {
                    errors.Should().BeEmpty("all of the examples in this theory should parse.");
                });
        }

        [Theory]
        [InlineData("")]
        [InlineData("-f 5 -t 1000")]
        [InlineData("--samples -forecast 5")]
        [InlineData("--samples 1 2 3 4 -forecast")]
        [InlineData("--samples 1 2 3 4 -forecast wibble")]
        public void Fail_To_Parse_If_Syntactically_Invalid(string args)
        {
            Parser.Default.ParseArguments<Options>(args.Split(' '))
                .WithParsed(options =>
                {
                    options.Should().BeNull("none of the example in this theory should parse.");
                })
                .WithNotParsed(errors =>
                {
                    errors.Should().NotBeEmpty();
                });
        }
    }
}
