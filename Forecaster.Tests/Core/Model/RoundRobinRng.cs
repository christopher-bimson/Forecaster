﻿using Forecaster.Core.Model;

namespace Forecaster.Tests.Core.Model
{
    internal class RepeatingRng : IRng
    {
        private readonly int[] randomNumbers;
        private int pointer;

        public RepeatingRng(int[] randomNumbers)
        {
            this.randomNumbers = randomNumbers;
            this.pointer = 0;
        }

        public int Next(int exclusiveUpperBound)
        {
            var value = randomNumbers[pointer];
            pointer = pointer < randomNumbers.Length - 1 ? pointer + 1 : 0;
            return value;
        }
    }
}