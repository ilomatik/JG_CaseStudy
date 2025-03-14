using System.Collections.Generic;
using Controllers;
using Enums;
using Events;
using Scripts;
using Scripts.Bet;
using Scripts.Helpers;
using UI;
using UI.Popup;
using UI.PopupConstants;
using UnityEngine;
using Views.Interfaces;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("Scene References")]
        [SerializeField] private string       _playerName;
        [SerializeField] private Transform    _viewContainer;
        [SerializeField] private Transform    _uiContainer;
        
        [Header("Game Objects")]
        [SerializeField] private GameObject   _gameView;
        [SerializeField] private GameObject   _gameUIView;
        [SerializeField] private GameSettings _gameSettings;
        
        [Header("Managers")]
        [SerializeField] private StorageManager _storageManager;
        [SerializeField] private BetManager     _betManager;
        [SerializeField] private PopupManager   _popupManager;

        private string           _playerNameHolder;
        private GameController   _gameController;
        private PayoutController _payoutController;
        private GameUIView       _gameUIViewComponent;
        
        private IStorageManager _storage;
        private IBetManager     _bet;
        private IPopupManager   _popup;

        private void Start()
        {
            GameObject gameView   = Instantiate(_gameView, _viewContainer);
            GameObject gameUIView = Instantiate(_gameUIView, _uiContainer);
            
            _storage = _storageManager;
            _bet     = _betManager;
            _popup   = _popupManager;
            
            _gameUIViewComponent = gameUIView.GetComponent<GameUIView>();
            _gameController      = new GameController(gameView.GetComponent<IGameView>(), _gameSettings);
            _payoutController    = new PayoutController();
            
            Initialize();
            
            _gameUIViewComponent.SetPlayerName(_playerNameHolder);
            
            SubscribeEvents();
            _gameController.SubscribeEvents();
            
            StartGame();
        }

        private void Initialize()
        {
            _storage.Initialize(_playerName);
            _bet    .Initialize();
            _popup  .Initialize();
            _gameController.Initialize();
        }
        
        private void StartGame()
        {
            _gameController.StartGame(_storage.GetPlayerChips());
        }

        private void SubscribeEvents()
        {
            GameEvents.OnChipAmountChanged += OnUpdateChipAmount;
            GameEvents.OnWheelSpinComplete += IsBetWinning;
            GameEvents.OnShopButtonClicked += ShowShopPopup;
            
            BetEvents.OnBetPlaced  += _bet.PlaceBet;
            BetEvents.OnBetRemoved += _bet.RemoveBet;
        }
        
        private void UnsubscribeEvents()
        {
            GameEvents.OnChipAmountChanged -= OnUpdateChipAmount;
            GameEvents.OnWheelSpinComplete -= IsBetWinning;
            GameEvents.OnShopButtonClicked -= ShowShopPopup;
            
            BetEvents.OnBetPlaced  -= _bet.PlaceBet;
            BetEvents.OnBetRemoved -= _bet.RemoveBet;
        }
        
        private void IsBetWinning(int winningNumber)
        {
            List<PlayerBet> winningBets = _bet.GetWinningBets(0, winningNumber);
            
            foreach (PlayerBet bet in winningBets)
            {
                BetType betType = bet.BetType;
                float   payout  = PayoutCalculator.CalculatePayoutAmount(betType, false, bet.TotalChipValue);
                _payoutController.PayoutToPlayer((int)payout);
                
                GameEvents.GameWin();
                
                ShowWinPopup(new PopupInfo("You won! You won " + payout + "chips!"));
                Debug.Log($"{bet.BetType} is winning! Payout: {payout}");
            }
        }
        
        private void OnUpdateChipAmount(string chipType, int amount)
        {
            _storage.UpdateChipAmount(chipType, amount);

            if (amount <= 0) return;
            
            ChipType chip = EnumComparison.GetChipType(chipType);
            _gameController.AddChipToHolder(chip);
        }
        
        private void ShowWinPopup(PopupInfo popupInfo)
        {
            _popup.ShowPopup(PopupNames.WinPopup, popupInfo);
        }
        
        private void ShowShopPopup()
        {
            _popup.ShowPopup(PopupNames.ShopPopup);
        }
        
        private void OnDestroy()
        {
            _gameController.UnsubscribeEvents();
            
            UnsubscribeEvents();
        }
    }
}