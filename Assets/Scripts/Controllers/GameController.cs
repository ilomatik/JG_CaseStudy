using Data;
using Events;
using Scripts;
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
            
            _data = new GameData();
        }
        
        public void Initialize()
        {
            _data.Initialize();
            _view.Initialize(_settings.TableSlotsCount, _settings.TableSlotSpacing, _settings.WheelSpeed);
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
            _view.SpinWheel(_settings.RandomizeWheelStopValue, _settings.WheelStopValue);
        }
        
        private void ShowResult(int value)
        {
            _view.ShowResult(value);
        }
    }
}