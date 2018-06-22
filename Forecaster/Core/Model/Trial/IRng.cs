namespace Forecaster.Core.Model.Trial
{
    public interface IRng
    {
        int Next(int exclusiveUpperBound);
    }
}
