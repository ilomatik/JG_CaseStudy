using Data;
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
    }
}