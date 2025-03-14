using System.Collections.Generic;
using Enums;
using Events;
using Scripts;
using Scripts.Helpers;
using UI;
using UnityEngine;
using Views.Chip;
using Views.Interfaces;

namespace Controllers
{
    public class GameController
    {
        private GameUIView   _uiView;
        private IGameView    _view;
        
        public GameController(IGameView view,  GameUIView uiView)
        {
            _view     = view;
            _uiView   = uiView;
        }
        
        public void Initialize()
        {
            _view.Initialize();
            _view.InitializeBetAreas();
        }
        
        public void SubscribeEvents()
        {
            GameEvents.OnSpinButtonClicked += SpinWheel;
            GameEvents.OnWheelSpinComplete += ShowResult;
            GameEvents.OnPlaceChip         += AddChip;
            GameEvents.OnRemoveChip        += RemoveChip;
        }
        
        public void UnsubscribeEvents()
        {
            GameEvents.OnSpinButtonClicked -= SpinWheel;
            GameEvents.OnWheelSpinComplete -= ShowResult;
            GameEvents.OnPlaceChip         -= AddChip;
            GameEvents.OnRemoveChip        -= RemoveChip;
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
            _view.SpinWheel(_uiView.GetWinningNumber());
        }
        
        private void AddChip(GameObject chip)
        {
            _view.PlaceChip(chip);
        }
        
        private void RemoveChip(ChipView chip)
        {
            _view.AddChipToHolder(chip);
        }
        
        private void ShowResult(int value)
        {
            _view.ShowResult(value);
            _view.ClearPlacedChips();
        }
    }
}