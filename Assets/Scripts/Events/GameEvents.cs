using System;
using System.Collections.Generic;
using Enums;
using Scripts.Helpers;
using UnityEngine;
using Views.Chip;

namespace Events
{
    public static class GameEvents
    {
        public static Action OnGameStart;
        public static Action OnGameEnd;
        
        public static Action<int, GameObject> OnChipPlaced;
        public static Action<int, GameObject> OnChipRemoved;

        public static Action      OnWheelStartSpin;
        public static Action      OnWheelStopSpin;
        public static Action<int> OnWheelSpinComplete;

        public static Action              OnSpinButtonClicked;
        public static Action<string, int> OnChipAmountChanged;

        public static void GameStart() { OnGameStart?.Invoke(); }
        public static void GameEnd()   { OnGameEnd?.Invoke(); }
        
        public static void PlaceChip(int playerId, GameObject chip)  { OnChipPlaced?.Invoke(playerId, chip); }
        public static void RemoveChip(int playerId, GameObject chip) { OnChipRemoved?.Invoke(playerId, chip); }
        
        public static void WheelStartSpin()                  { OnWheelStartSpin?.Invoke(); }
        public static void WheelStopSpin()                   { OnWheelStopSpin?.Invoke(); }
        public static void WheelSpinComplete(int stopNumber) { OnWheelSpinComplete?.Invoke(stopNumber); }
        
        public static void ClickSpinButton()                               { OnSpinButtonClicked?.Invoke(); }
        public static void ChangeChipAmount(string chipAmount, int amount) { OnChipAmountChanged?.Invoke(chipAmount, amount); }
    }
    
    public static class BetEvents
    {
        public static event Action<int, BetType, List<int>, ChipType> OnBetPlaced;
        public static event Action<int, BetType, List<int>, ChipType> OnBetRemoved;

        public static void PlaceBet(int playerId, BetType betType, List<int> numbers, ChipType chipValue)  { OnBetPlaced?.Invoke(playerId, betType, numbers, chipValue); }

        public static void RemoveBet(int playerId, BetType betType, List<int> numbers, ChipType chipValue) { OnBetRemoved?.Invoke(playerId, betType, numbers, chipValue); }
    }
}