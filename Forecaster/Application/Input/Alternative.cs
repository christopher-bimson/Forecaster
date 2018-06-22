namespace Forecaster.Application.Input
{
    public class Alternative<THappyPath, TSadPath>
    {
        public THappyPath SuccessResult { get; private set; }
        public TSadPath FailResult { get; private set; }
        public bool IsSuccess { get; private set; }

        public Alternative(THappyPath success)
        {
            SuccessResult = success;
            IsSuccess = true;
        }

        public Alternative(TSadPath failure)
        {
            FailResult = failure;
            IsSuccess = false;
        }
    }
}