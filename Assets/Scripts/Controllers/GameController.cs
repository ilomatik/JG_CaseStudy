using System.Collections.Generic;
using Data;
using Enums;
using Events;
using Scripts;
using Scripts.Helpers;
using UnityEngine;
using Views.Interfaces;

namespace Controllers
{
    public class GameController
    {
        private GameData     _data;
        private IGameView    _view;
        private GameSettings _settings;
        
        public GameController(IGameView view, GameSettings settings)
        {
            _view     = view;
            _settings = settings;
            _data     = new GameData();
        }
        
        public void Initialize()
        {
            _data.Initialize();
            _view.Initialize();
            _view.InitializeBetAreas();
            
            _view.SetWheelSpinDuration(_settings.WheelSpeed);
        }
        
        public void SubscribeEvents()
        {
            GameEvents.OnSpinButtonClicked += SpinWheel;
            GameEvents.OnWheelSpinComplete += ShowResult;
            GameEvents.OnPlaceChip         += AddChip;
        }
        
        public void UnsubscribeEvents()
        {
            GameEvents.OnSpinButtonClicked -= SpinWheel;
            GameEvents.OnWheelSpinComplete -= ShowResult;
            GameEvents.OnPlaceChip         -= AddChip;
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
        
        public void AddChipToHolder(ChipType chipType)
        {
            _view.SpawnChip(chipType);
        }

        private void SpinWheel()
        {
            _view.SpinWheel(_settings.RandomizeWheelStopValue, _settings.WheelStopValue);
        }
        
        private void AddChip(GameObject chip)
        {
            _view.PlaceChip(chip);
        }
        
        private void ShowResult(int value)
        {
            _view.ShowResult(value);
            _view.ClearPlacedChips();
        }
    }
}