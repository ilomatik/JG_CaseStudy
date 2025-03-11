namespace Views.Interfaces
{
    public interface IWheelView
    {
        void Initialize();
        void SetStopNumber(bool isRandomize, int stopNumber);
        void SetSpinDuration(float spinSpeed);
        void Spin();
    }
}