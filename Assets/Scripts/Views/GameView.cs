using System.Collections.Generic;
using UnityEngine;

namespace Views
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private GameObject       _wheelObject;
        [SerializeField] private GameObject       _tableObject;
        [SerializeField] private List<GameObject> _betAreas;

        private WheelView _wheel;
        private TableView _table;

        public void Initialize(float wheelSpeed)
        {
            _wheel = _wheelObject.GetComponent<WheelView>();
            _table = _tableObject.GetComponent<TableView>();

            _table.Initialize();
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