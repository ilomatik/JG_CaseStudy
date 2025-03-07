using Data;
using Events;
using Scripts;
using Scripts.Helpers;
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
            _view.Initialize(_settings.WheelSpeed);
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
        }

        private void SpinWheel()
        {
            _view.SpinWheel(_settings.RandomizeWheelStopValue, _settings.WheelStopValue);
        }
        
        private void ShowResult(int value)
        {
            _view.ShowResult(value);
        }
        
        private void AddBet(BetHolder bet)
        {
            _data.AddBet(bet);
        }
        
        private void RemoveBet(BetHolder bet)
        {
            _data.RemoveBet(bet);
        }
        
        private void ClearBets()
        {
            _data.ClearBets();
        }

        private AllBetHolder GetAllBets()
        {
            return _data.GetAllBets();
        }
    }
}