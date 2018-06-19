using Forecaster.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class DoubleExtensions
    {
        public static double[] SampleWithReplacement(this double[] samples, int count, 
            IRng rng)
        {
            var result = new double[count];
            for(int i = 0; i < count; i++)
            {
                result[i] = samples[rng.Next(samples.Length)];
            }
            return result;
        }
    }
}
