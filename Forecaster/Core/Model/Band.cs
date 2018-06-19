namespace Forecaster.Core.Model
{
    public struct Band
    {
        public Band(double likelihood, double value) : this()
        {
            Likelihood = likelihood;
            Value = value;
        }

        public double Likelihood { get; private set; }

        public double Value { get; private set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Band))
                return false;

            var other = (Band)obj;

            return other.Likelihood == Likelihood &&
                other.Value == Value;
        }

        public override int GetHashCode()
        {
            var hashCode = 2013874805;
            hashCode = hashCode * -1521134295 + Likelihood.GetHashCode();
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            return hashCode;
        }
    }
}
