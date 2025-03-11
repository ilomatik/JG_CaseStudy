using System.Collections.Generic;
using Data;
using Enums;
using Events;
using Scripts;
using Scripts.Helpers;
using UnityEngine;
using Views;

namespace Controllers
{
    public class GameController
    {
        private GameData     _data;
        private GameView     _view;
        private GameSettings _settings;
        
        public GameController(GameView view, GameSettings settings)
        {
            _view     = view;
            _settings = settings;
            _data     = new GameData();
        }
        
        public void Initialize()
        {
            _data.Initialize();
            _view.Initialize();
            
            _view.SetWheelSpinDuration(_settings.WheelSpeed);
        }
        
        public void SubscribeEvents()
        {
            GameEvents.OnSpinButtonClicked += SpinWheel;
            GameEvents.OnWheelSpinComplete += ShowResult;
            GameEvents.OnChipPlaced        += AddBet;
            GameEvents.OnChipRemoved       += RemoveBet;
        }
        
        public void UnsubscribeEvents()
        {
            GameEvents.OnSpinButtonClicked -= SpinWheel;
            GameEvents.OnWheelSpinComplete -= ShowResult;
            GameEvents.OnChipPlaced        -= AddBet;
            GameEvents.OnChipRemoved       -= RemoveBet;
        }
        
        public void StartGame(List<ChipData> playerChips)
        {
            foreach (ChipData playerChip in playerChips)
            {
                int chipAmount = playerChip.amount;

                for (int c = 0; c < chipAmount; c++)
                {
                    ChipType chipType = EnumComparison.GetChipType(playerChip.chipType);
                
                    _view.SpawnChip(chipType);
                }
            }
        }

        private void SpinWheel()
        {
            _view.SpinWheel(_settings.RandomizeWheelStopValue, _settings.WheelStopValue);
        }
        
        private void ShowResult(int value)
        {
            _view.ShowResult(value);
        }
        
        private void AddBet(int betAreaId, GameObject chip)
        {
            _view.PlaceChip(betAreaId, chip);
        }
        
        private void RemoveBet(int betAreaId, GameObject chip)
        {
            _view.RemoveChip(betAreaId, chip);
        }
        
        private void ClearBets()
        {
            _data.ClearBets();
        }
    }
}