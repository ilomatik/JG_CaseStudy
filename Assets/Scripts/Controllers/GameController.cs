using Data;
using Events;
using Views;

namespace Controllers
{
    public class GameController
    {
        private GameData _data;
        private GameView _view;
        
        public GameController(GameView view)
        {
            _view = view;
            
            _data = new GameData();
        }
        
        public void Initialize()
        {
            _data.Initialize();
            _view.Initialize();
        }
        
        public void SubscribeEvents()
        {
            GameEvents.OnSpinButtonClicked += SpinWheel;
            GameEvents.OnWheelSpinComplete += ShowResult;
        }
        
        public void UnsubscribeEvents()
        {
            GameEvents.OnSpinButtonClicked -= SpinWheel;
            GameEvents.OnWheelSpinComplete -= ShowResult;
        }

        private void SpinWheel()
        {
            _view.SpinWheel();
        }
        
        private void ShowResult(int value)
        {
            _view.ShowResult(value);
        }
    }
}