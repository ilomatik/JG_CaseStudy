using UnityEngine;

namespace Views.Interfaces
{
    public interface ITableSlotView
    {
        int SlotNumber { get; }
        void Initialize(Material winningMaterial);
        void ChangeWinningColor();
        void ChangeDefaultColor();
        void ShowWinning();
    }
}