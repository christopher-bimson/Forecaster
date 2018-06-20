namespace Forecaster.Core.Model
{
    public struct Bucket
    {
        public Bucket(decimal likelihood, double value) : this()
        {
            Likelihood = likelihood;
            Value = value;
        }

        public decimal Likelihood { get; private set; }

        public double Value { get; private set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Bucket))
                return false;

            var other = (Bucket)obj;

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
