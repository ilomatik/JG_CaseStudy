namespace Managers
{
    public interface IAudioManager
    {
        void PlayOnChipDrop();
        void PlayOnWin();
        void PlayOnLose();
        void PlayOnButtonClick();
    }
}