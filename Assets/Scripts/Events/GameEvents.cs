using System;
using Scripts.Helpers;

namespace Events
{
    public static class GameEvents
    {
        public static Action OnGameStart;
        public static Action OnGameEnd;
        
        public static Action<BetHolder> OnChipPlaced;
        public static Action<BetHolder> OnChipRemoved;

        public static Action      OnWheelStartSpin;
        public static Action      OnWheelStopSpin;
        public static Action<int> OnWheelSpinComplete;

        public static Action              OnSpinButtonClicked;
        public static Action<string, int> OnChipAmountChanged;

        public static void GameStart() { OnGameStart?.Invoke(); }
        public static void GameEnd()   { OnGameEnd?.Invoke(); }
        
        public static void PlaceChip(BetHolder bet)  { OnChipPlaced?.Invoke(bet); }
        public static void RemoveChip(BetHolder bet) { OnChipRemoved?.Invoke(bet); }
        
        public static void WheelStartSpin()                  { OnWheelStartSpin?.Invoke(); }
        public static void WheelStopSpin()                   { OnWheelStopSpin?.Invoke(); }
        public static void WheelSpinComplete(int stopNumber) { OnWheelSpinComplete?.Invoke(stopNumber); }
        
        public static void ClickSpinButton()                               { OnSpinButtonClicked?.Invoke(); }
        public static void ChangeChipAmount(string chipAmount, int amount) { OnChipAmountChanged?.Invoke(chipAmount, amount); }
    }
}