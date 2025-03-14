using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Events
{
    public static class GameEvents
    {
        // Declare the event
        public static Action OnGameStart;
        public static Action OnGameEnd;
        public static Action OnPopupOpen;
        public static Action OnPopupClose;
        
        public static Action OnGameWin;
        public static Action OnGameLose;

        public static Action      OnWheelStartSpin;
        public static Action      OnWheelStopSpin;
        public static Action<int> OnWheelSpinComplete;
        
        public static Action<GameObject> OnPlaceChip;

        public static Action              OnSpinButtonClicked;
        public static Action<string, int> OnChipAmountChanged;
        
        public static Action OnShopButtonClicked;
        
        
        // Invoke the event

        public static void GameStart() { OnGameStart?.Invoke(); }
        public static void GameEnd()   { OnGameEnd?.Invoke(); }
        public static void PopupOpen() { OnPopupOpen?.Invoke(); }
        public static void PopupClose(){ OnPopupClose?.Invoke(); }
        
        public static void GameWin()  { OnGameWin?.Invoke(); }
        public static void GameLose() { OnGameLose?.Invoke(); }
        
        public static void WheelStartSpin()                  { OnWheelStartSpin?.Invoke(); }
        public static void WheelStopSpin()                   { OnWheelStopSpin?.Invoke(); }
        public static void WheelSpinComplete(int stopNumber) { OnWheelSpinComplete?.Invoke(stopNumber); }
        
        public static void PlaceChip(GameObject chip) { OnPlaceChip?.Invoke(chip); }
        
        public static void ClickSpinButton()                               { OnSpinButtonClicked?.Invoke(); }
        public static void ChangeChipAmount(string chipAmount, int amount) { OnChipAmountChanged?.Invoke(chipAmount, amount); }
        
        public static void ClickShopButton() { OnShopButtonClicked?.Invoke(); }
        
    }
    
    public static class BetEvents
    {
        public static event Action<int, BetType, List<int>, ChipType> OnBetPlaced;
        public static event Action<int, BetType, List<int>, ChipType> OnBetRemoved;

        public static void PlaceBet(int playerId, BetType betType, List<int> numbers, ChipType chipValue)  { OnBetPlaced?.Invoke(playerId, betType, numbers, chipValue); }

        public static void RemoveBet(int playerId, BetType betType, List<int> numbers, ChipType chipValue) { OnBetRemoved?.Invoke(playerId, betType, numbers, chipValue); }
    }
}