using System;

namespace Events
{
    public static class GameEvents
    {
        public static Action OnGameStart;
        public static Action OnGameEnd;
        
        public static Action OnChipSelected;
        public static Action OnChipDeselected;
        public static Action OnChipMoved;
        
        public static Action OnWheelStartSpin;
        public static Action OnWheelStopSpin;

        public static Action OnSpinButtonClicked;
        
        public static void GameStart() { OnGameStart?.Invoke(); }
        public static void GameEnd()   { OnGameEnd?.Invoke(); }
        
        public static void ChipSelected()   { OnChipSelected?.Invoke(); }
        public static void ChipDeselected() { OnChipDeselected?.Invoke(); }
        public static void ChipMoved()      { OnChipMoved?.Invoke(); }
        
        public static void WheelStartSpin() { OnWheelStartSpin?.Invoke(); }
        public static void WheelStopSpin()  { OnWheelStopSpin?.Invoke(); }
        
        public static void ClickSpinButton() { OnSpinButtonClicked?.Invoke(); }
    }
}