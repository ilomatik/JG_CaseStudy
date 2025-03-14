using Enums;
using UnityEngine;
using Views.Chip;

namespace Views.Interfaces
{
    public interface IGameView
    {
        void Initialize();
        void InitializeBetAreas();
        void SetWheelSpinDuration(float duration);
        void SpawnChip(ChipType chipType);
        void SpinWheel(bool isRandom, int wheelStopValue);
        void ShowResult(int value);
        void AddChipToHolder(ChipView chipView);
        void PlaceChip(GameObject chip);
        void RemoveChipFromBetArea(int betAreaId, GameObject chip);
        void ClearPlacedChips();
    }
}