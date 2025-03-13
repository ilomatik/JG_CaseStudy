using Enums;
using UnityEngine;

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
        void PlaceChip(GameObject chip);
        void RemoveChip(int betAreaId, GameObject chip);
        void ClearPlacedChips();
    }
}