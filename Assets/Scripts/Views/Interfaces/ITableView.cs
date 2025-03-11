namespace Views.Interfaces
{
    public interface ITableView
    {
        void Initialize();
        void ShowWinningSlot(int winningNumber);
        void ResetSlots();
    }
}