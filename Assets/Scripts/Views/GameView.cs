using UnityEngine;

namespace Views
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private GameObject _wheelObject;
        [SerializeField] private GameObject _tableObject;
        [SerializeField] private int        _wheelStopValue;
        [SerializeField] private float      _wheelSpeed;

        private WheelView _wheel;
        private TableView _table;

        public void Initialize()
        {
            _wheel = _wheelObject.GetComponent<WheelView>();
            _table = _tableObject.GetComponent<TableView>();
            
            _table.Initialize();
            _wheel.Initialize();
            _wheel.SetSpinDuration(_wheelSpeed);
        }
        
        public void SpinWheel()
        {
            _table.ResetSlots();
            _wheel.SetStopNumber(_wheelStopValue);
            _wheel.Spin();
        }
        
        public void ShowResult(int value)
        {
            Debug.Log($"Result: {value}");
            _table.ShowWinningSlot(value);
        }
    }
}