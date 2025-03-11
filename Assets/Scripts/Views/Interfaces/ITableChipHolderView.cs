using Views.Chip;

namespace Views.Interfaces
{
    public interface ITableChipHolderView
    {
        void Initialize();
        void AddChipToHolder(ChipView chipView);
    }
}