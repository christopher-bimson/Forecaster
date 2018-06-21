namespace Forecaster.Application.Input
{
    public class Alternative<THappyPath, TSadPath>
    {
        public THappyPath Success { get; private set; }
        public TSadPath Failure { get; private set; }
        public bool IsSuccess { get; private set; }

        public Alternative(THappyPath success)
        {
            Success = success;
            IsSuccess = true;
        }

        public Alternative(TSadPath failure)
        {
            Failure = failure;
            IsSuccess = false;
        }
    }
}