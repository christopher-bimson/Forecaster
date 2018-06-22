using FluentAssertions;
using Forecaster.Core.Model.Trial;
using Xunit;

namespace Forecaster.Tests.Core.Model
{
    public class TrialsShould
    {
        [Fact]
        public void Be_Implicitly_Convertable_From_A_Double_Array()
        {
            double[] data = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            Trials trials = data;

            data.Should().BeEquivalentTo(trials);
        }

        [Fact]
        public void Be_Implicitly_Covertanle_To_A_Double_Array()
        {
            var trials = new Trials(new[] { 1.0, 2.0, 3.0});
            double[] data = trials;

            trials.Should().BeEquivalentTo(data);
        }

        [Fact]
        public void Report_The_Trial_Count_Accurately()
        {
            double[] data = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            Trials trials = data;

            trials.Count.Should().Be(data.Length);
        }

        [Fact]
        public void Report_The_Biggest_Trial_Accurately()
        {
            double[] data = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            Trials trials = data;

            trials.Max.Should().Be(5);
        }

        [Fact]
        public void Report_The_Smallest_Trial_Accurately()
        {
            double[] data = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            Trials trials = data;

            trials.Min.Should().Be(1);
        }

        [Fact] 
        public void Calculate_The_Likelihood_Of_A_Given_Value()
        {
            double[] data = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            Trials trials = data;

            trials.CalculateLikelihoodOf(3.0).Should().Be(60m);
        }
    }
}
