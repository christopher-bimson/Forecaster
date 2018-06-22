using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Forecaster.Core.Model.Summary
{
    public class Buckets : IEnumerable<Bucket>
    {
        private readonly Dictionary<decimal, int> buckets;

        public Buckets()
        {
            buckets = new Dictionary<decimal, int>();
        }

        public void Add(decimal likelihood, int forecast)
        {
            if (likelihood == 0)
                return;

            if (!buckets.ContainsKey(likelihood))
            {
                buckets.Add(likelihood, forecast);
            }

            if (buckets[likelihood] < forecast)
            {
                buckets[likelihood] = forecast;
            }
        }

        public IEnumerator<Bucket> GetEnumerator()
        {
            return buckets.Select(kvp => new Bucket(kvp.Key, kvp.Value)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
