namespace Forecaster.Domain
{
    internal class BandLikelihood
    {
        internal BandLikelihood(int band, double likelihood)
        {
            Band = band;
            Likelihood = likelihood;
        }

        internal int Band { get; private set; }
        internal double Likelihood { get; private set; }
    }
}