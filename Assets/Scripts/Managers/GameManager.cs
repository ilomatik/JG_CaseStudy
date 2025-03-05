using Controllers;
using UI;
using UnityEngine;
using Views;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Transform  _viewContainer;
        [SerializeField] private Transform  _uiContainer;
        [SerializeField] private GameObject _gameView;
        [SerializeField] private GameObject _gameUIView;

        private GameController _gameController;
        private GameUIView     _gameUIViewComponent;

        private void Start()
        {
            GameObject gameView   = Instantiate(_gameView, _viewContainer);
            GameObject gameUIView = Instantiate(_gameUIView, _uiContainer);
            
            _gameController = new GameController(gameView.GetComponent<GameView>());
            _gameController.Initialize();
            _gameController.SubscribeEvents();
            
            _gameUIViewComponent = gameUIView.GetComponent<GameUIView>();
        }
        
        private void OnDestroy()
        {
            _gameController.UnsubscribeEvents();
        }
    }
}