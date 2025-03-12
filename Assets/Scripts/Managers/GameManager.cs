using System.Collections.Generic;
using Controllers;
using Events;
using Scripts;
using Scripts.Bet;
using UI;
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
        
        
        private string         _playerNameHolder;
        private GameController _gameController;
        private GameUIView     _gameUIViewComponent;

        private void Start()
        {
            GameObject gameView   = Instantiate(_gameView, _viewContainer);
            GameObject gameUIView = Instantiate(_gameUIView, _uiContainer);
            
            _gameUIViewComponent = gameUIView.GetComponent<GameUIView>();
            _gameController      = new GameController(gameView.GetComponent<IGameView>(), _gameSettings);
            
            Initialize();
            
            _gameUIViewComponent.SetPlayerName(_playerNameHolder);
            
            SubscribeEvents();
            _gameController.SubscribeEvents();
            
            StartGame();
        }

        private void Initialize()
        {
            _storageManager.Initialize(_playerName);
            _betManager    .Initialize();
            _gameController.Initialize();
        }
        
        private void StartGame()
        {
            _gameController.StartGame(_storageManager.GetPlayerChips());
        }

        private void SubscribeEvents()
        {
            GameEvents.OnChipAmountChanged += _storageManager.UpdateChipAmount;
            
            BetEvents.OnBetPlaced  += _betManager.PlaceBet;
            BetEvents.OnBetRemoved += _betManager.RemoveBet;
            
            GameEvents.OnWheelSpinComplete += IsBetWinning;
        }
        
        private void UnsubscribeEvents()
        {
            GameEvents.OnChipAmountChanged -= _storageManager.UpdateChipAmount;
            
            BetEvents.OnBetPlaced  -= _betManager.PlaceBet;
            BetEvents.OnBetRemoved -= _betManager.RemoveBet;
            
            GameEvents.OnWheelSpinComplete -= IsBetWinning;
        }
        
        private void IsBetWinning(int winningNumber)
        {
            List<PlayerBet> winningBets = _betManager.GetWinningBets(0, winningNumber);
            
            foreach (PlayerBet bet in winningBets)
            {
                Debug.Log($"{bet.BetType} is winning!");
            }
        }
        
        private void OnDestroy()
        {
            _gameController.UnsubscribeEvents();
            
            UnsubscribeEvents();
        }
    }
}