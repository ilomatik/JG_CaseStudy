using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using Views.Chip;

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
        public static Action<ChipView>   OnRemoveChip;

        public static Action              OnSpinButtonClicked;
        public static Action<string, int> OnChipAmountChanged;
        
        public static Action OnShopButtonClicked;
        public static Action OnStatisticsButtonClicked;
        
        
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
        public static void RemoveChip(ChipView chip)  { OnRemoveChip?.Invoke(chip); }
        
        public static void ClickSpinButton()                               { OnSpinButtonClicked?.Invoke(); }
        public static void ChangeChipAmount(string chipAmount, int amount) { OnChipAmountChanged?.Invoke(chipAmount, amount); }
        
        public static void ClickShopButton()       { OnShopButtonClicked?.Invoke(); }
        public static void ClickStatisticsButton() { OnStatisticsButtonClicked?.Invoke(); }
        
    }
    
    public static class BetEvents
    {
        public static event Action<int, BetType, List<int>, ChipType> OnBetPlaced;
        public static event Action<int, BetType, List<int>, ChipType> OnBetRemoved;

        public static void PlaceBet(int playerId, BetType betType, List<int> numbers, ChipType chipValue)  { OnBetPlaced?.Invoke(playerId, betType, numbers, chipValue); }

        public static void RemoveBet(int playerId, BetType betType, List<int> numbers, ChipType chipValue) { OnBetRemoved?.Invoke(playerId, betType, numbers, chipValue); }
    }
    
    public static class ParticleEvents
    {
        public static event Action<Vector3> OnDropChipParticle;
        public static event Action<Vector3> OnWinParticle;
        public static event Action<Vector3> OnNumberWinParticle;
        public static event Action<Vector3> OnBallStopOnWheelSlotParticle;

        public static void DropChipParticle(Vector3 position)           { OnDropChipParticle?.Invoke(position); }
        public static void WinParticle(Vector3 position)                { OnWinParticle?.Invoke(position); }
        public static void NumberWinParticle(Vector3 position)          { OnNumberWinParticle?.Invoke(position); }
        public static void BallStopOnWheelSlotParticle(Vector3 position) { OnBallStopOnWheelSlotParticle?.Invoke(position); }
    }
}