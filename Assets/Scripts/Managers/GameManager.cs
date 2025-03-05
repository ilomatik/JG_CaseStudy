using System;
using Controllers;
using UnityEngine;
using Views;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Transform  _viewContainer;
        [SerializeField] private GameObject _gameView;

        private GameController _gameController;

        private void Start()
        {
            GameObject gameView = Instantiate(_gameView, _viewContainer);
            _gameController = new GameController(gameView.GetComponent<GameView>());
            
        }
    }
}