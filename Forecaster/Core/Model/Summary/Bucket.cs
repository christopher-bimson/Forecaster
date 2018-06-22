namespace Forecaster.Core.Model.Summary
{
    public struct Bucket
    {
        public Bucket(decimal likelihood, double forecast) : this()
        {
            Likelihood = likelihood;
            Forecast = forecast;
        }

        public decimal Likelihood { get; private set; }

        public double Forecast { get; private set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Bucket))
                return false;

            var other = (Bucket)obj;

            return other.Likelihood == Likelihood &&
                other.Forecast == Forecast;
        }

        public override int GetHashCode()
        {
            var hashCode = 2013874805;
            hashCode = hashCode * -1521134295 + Likelihood.GetHashCode();
            hashCode = hashCode * -1521134295 + Forecast.GetHashCode();
            return hashCode;
        }
    }
}
