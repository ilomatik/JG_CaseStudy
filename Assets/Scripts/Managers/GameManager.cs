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
        [SerializeField] private StorageManager  _storageManager;
        [SerializeField] private BetManager      _betManager;
        [SerializeField] private PopupManager    _popupManager;
        [SerializeField] private ParticleManager _particleManager;
        

        private string           _playerNameHolder;
        private GameController   _gameController;
        private PayoutController _payoutController;
        private GameUIView       _gameUIViewComponent;
        
        private IStorageManager  _storage;
        private IBetManager      _bet;
        private IPopupManager    _popup;
        private IParticleManager _particle;

        private void Start()
        {
            GameObject gameView   = Instantiate(_gameView, _viewContainer);
            GameObject gameUIView = Instantiate(_gameUIView, _uiContainer);
            
            _storage  = _storageManager;
            _bet      = _betManager;
            _popup    = _popupManager;
            _particle = _particleManager;
            
            _gameUIViewComponent = gameUIView.GetComponent<GameUIView>();
            _gameController      = new GameController(gameView.GetComponent<IGameView>(), _gameSettings, _gameUIViewComponent);
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
            GameEvents.OnChipAmountChanged       += OnUpdateChipAmount;
            GameEvents.OnWheelSpinComplete       += IsBetWinning;
            GameEvents.OnShopButtonClicked       += ShowShopPopup;
            GameEvents.OnStatisticsButtonClicked += ShowStatisticsPopup;
            
            ParticleEvents.OnDropChipParticle            += PlayOnDropChipParticle;
            ParticleEvents.OnWinParticle                 += PlayOnWinParticle;
            ParticleEvents.OnNumberWinParticle           += PlayOnNumberWinParticle;
            ParticleEvents.OnBallStopOnWheelSlotParticle += PlayOnBallStopOnWheelSlotParticle;
            
            BetEvents.OnBetPlaced  += _bet.PlaceBet;
            BetEvents.OnBetRemoved += _bet.RemoveBet;
        }
        
        private void UnsubscribeEvents()
        {
            GameEvents.OnChipAmountChanged       -= OnUpdateChipAmount;
            GameEvents.OnWheelSpinComplete       -= IsBetWinning;
            GameEvents.OnShopButtonClicked       -= ShowShopPopup;
            GameEvents.OnStatisticsButtonClicked -= ShowStatisticsPopup;
            
            ParticleEvents.OnDropChipParticle            -= PlayOnDropChipParticle;
            ParticleEvents.OnWinParticle                 -= PlayOnWinParticle;
            ParticleEvents.OnNumberWinParticle           -= PlayOnNumberWinParticle;
            ParticleEvents.OnBallStopOnWheelSlotParticle -= PlayOnBallStopOnWheelSlotParticle;
            
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
                
                _storage.IncrementGamesWon();
                ShowWinPopup(new PopupInfo("You won! You won " + payout + "chips!"));
                PlayOnWinParticle(Vector3.zero);
                Debug.Log($"{bet.BetType} is winning! Payout: {payout}");
            }
            
            if (winningBets.Count == 0)
            {
                GameEvents.GameLose();
                _storage.IncrementGamesLost();
                ShowLosePopup(new PopupInfo("You lost!"));
                Debug.Log("No winning bets.");
            }
            
            _storage.IncrementGamesPlayed();
        }
        
        private void OnUpdateChipAmount(string chipType, int amount)
        {
            _storage.UpdateChipAmount(chipType, amount);

            if (amount <= 0) return;

            for (int c = 0; c < amount; c++)
            {
                ChipType chip = EnumComparison.GetChipType(chipType);
                _gameController.AddChipToHolder(chip);
            }
        }

        #region Popups

        private void ShowWinPopup(PopupInfo popupInfo)
        {
            _popup.ShowPopup(PopupNames.WinPopup, popupInfo);
        }
        
        private void ShowLosePopup(PopupInfo popupInfo)
        {
            _popup.ShowPopup(PopupNames.LosePopup, popupInfo);
        }
        
        private void ShowShopPopup()
        {
            _popup.ShowPopup(PopupNames.ShopPopup);
        }
        
        private void ShowStatisticsPopup()
        {
            string playerName  = _storage.Player._playerName;
            int    gamesWon    = _storage.Player._gamesWon;
            int    gamesLost   = _storage.Player._gamesLost;
            int    gamesPlayed = _storage.Player._gamesPlayed;
            string lastLogin   = _storage.Player._lastLoginTime;
            
            string stats = $"Player: {playerName}\n" +
                           $"Games Won: {gamesWon}\n" +
                           $"Games Lost: {gamesLost}\n" +
                           $"Games Played: {gamesPlayed}\n" +
                           $"Last Login: {lastLogin}";
            
            _popup.ShowPopup(PopupNames.StatsPopup, new PopupInfo(stats));
        }
        
        #endregion

        #region Particle Effects
        
        private void PlayOnDropChipParticle(Vector3 position)
        {
            Debug.Log("Playing Drop Chip Particle");
            _particle.PlayOnDropChipParticle(position);
        }
        
        private void PlayOnWinParticle(Vector3 position)
        {
            Debug.Log("Playing Win Particle");
            _particle.PlayOnWinParticle(position);
        }
        
        private void PlayOnNumberWinParticle(Vector3 position)
        {
            Debug.Log("Playing Number Win Particle");
            _particle.PlayOnNumberWinParticle(position);
        }

        private void PlayOnBallStopOnWheelSlotParticle(Vector3 position)
        {
            Debug.Log("Playing Ball Stop On Wheel Slot Particle");
            _particle.PlayOnBallStopOnWheelSlotParticle(position);
        }

        #endregion
        
        private void OnDestroy()
        {
            _gameController.UnsubscribeEvents();
            
            UnsubscribeEvents();
        }
    }
}