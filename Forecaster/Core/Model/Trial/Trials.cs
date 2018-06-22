using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Forecaster.Core.Model.Trial
{
    public class Trials : IEnumerable<double>
    {
        public static implicit operator double[] (Trials t)
        {
            return t.ToArray();
        }

        public static implicit operator Trials(double[] data)
        {
            return new Trials(data);
        }

        private readonly double[] data;

        public int Count => data.Length;

        public double this[int index]
        {
            get
            {
                return data[index];
            }
        }

        public double Max
        {
            get; private set;
        }

        public double Min
        {
            get; private set;
        }

        public Trials(IEnumerable<double> data)
        {
            this.data = data.ToArray();
            Max = data.Max();
            Min = data.Min();
        }

        public IEnumerator<double> GetEnumerator()
        {
            return data.AsQueryable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        public decimal CalculateLikelihoodOf(double value)
        {
            return Math.Round((ThatMeetOrExceed(value) / (decimal)Count) * 100, 2);
        }

        private int ThatMeetOrExceed(double value)
        {
            return data.Where(t => t >= value).Count();
        }
    }
}
