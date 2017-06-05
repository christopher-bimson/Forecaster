namespace Forecaster.Domain
{
    public class BandLikelihood
    {
        internal BandLikelihood(int band, double likelihood)
        {
            Band = band;
            Likelihood = likelihood;
        }

        public int Band { get; private set; }
        public double Likelihood { get; private set; }
    }
}