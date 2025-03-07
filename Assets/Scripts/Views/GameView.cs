using UnityEngine;

namespace Views
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private GameObject _wheelObject;
        [SerializeField] private GameObject _tableObject;

        private WheelView _wheel;
        private TableView _table;

        public void Initialize(int slotCount, float slotSpacing, float wheelSpeed)
        {
            _wheel = _wheelObject.GetComponent<WheelView>();
            _table = _tableObject.GetComponent<TableView>();

            _table.Initialize(slotCount, slotSpacing);
            _wheel.Initialize();
            _wheel.SetSpinDuration(wheelSpeed);
        }
        
        public void SpinWheel(bool isRandom, int wheelStopValue)
        {
            _table.ResetSlots();
            _wheel.SetStopNumber(isRandom, wheelStopValue);
            _wheel.Spin();
        }
        
        public void ShowResult(int value)
        {
            _table.ShowWinningSlot(value);
        }
    }
}