using FluentAssertions;
using System;
using Xunit;

namespace Forecaster.Tests.Core.Model
{
    public class SamplingWithReplacementShould
    {
        [Fact]
        public void Sample_All_Values_When_Using_A_Repeating_Rng()
        {
            var rng = new RepeatingRng(new[] { 0, 1, 2, 3, 4 });
            
            var samples = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            var picked = samples.SampleWithReplacement(5, rng);

            picked.Should().BeEquivalentTo(picked);
        }
    }
}
