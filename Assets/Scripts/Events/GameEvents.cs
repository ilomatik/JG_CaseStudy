using System;

namespace Events
{
    public class GameEvents
    {
        public static Action OnGameStart;
        public static Action OnGameEnd;
        
        public static Action OnChipSelected;
        public static Action OnChipDeselected;
        public static Action OnChipMoved;
        
        public static void GameStart() { OnGameStart?.Invoke(); }
        public static void GameEnd()   { OnGameEnd?.Invoke(); }
        
        public static void ChipSelected()   { OnChipSelected?.Invoke(); }
        public static void ChipDeselected() { OnChipDeselected?.Invoke(); }
        public static void ChipMoved()      { OnChipMoved?.Invoke(); }
    }
}