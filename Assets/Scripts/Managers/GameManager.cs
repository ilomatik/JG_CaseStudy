using Controllers;
using Events;
using Scripts;
using UI;
using UnityEngine;
using Views;

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
        
        private string         _playerNameHolder;
        private GameController _gameController;
        private GameUIView     _gameUIViewComponent;

        private void Start()
        {
            GameObject gameView   = Instantiate(_gameView, _viewContainer);
            GameObject gameUIView = Instantiate(_gameUIView, _uiContainer);
            
            _gameController = new GameController(gameView.GetComponent<GameView>(), _gameSettings);
            _gameController.Initialize();
            _gameController.SubscribeEvents();
            

            if (_storageManager.LoadPlayerData())
            {
                _playerNameHolder = _storageManager._player._playerName;
            }
            else
            {
                _playerNameHolder = _playerName;
                _storageManager.CreatePlayerData(_playerNameHolder);
            }
            
            _gameUIViewComponent = gameUIView.GetComponent<GameUIView>();
            _gameUIViewComponent.SetPlayerName(_playerNameHolder);
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            GameEvents.OnChipAmountChanged += _storageManager.UpdateChipAmount;
        }
        
        private void UnsubscribeEvents()
        {
            GameEvents.OnChipAmountChanged -= _storageManager.UpdateChipAmount;
        }
        
        private void OnDestroy()
        {
            UnsubscribeEvents();
            _gameController.UnsubscribeEvents();
        }
    }
}