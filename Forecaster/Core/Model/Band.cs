namespace Forecaster.Core.Model
{
    public struct Band
    {
        public Band(double likelihood, double value) : this()
        {
        }

        public double Likelihood { get; private set; }
        public double Value { get; private set; }
    }
}
