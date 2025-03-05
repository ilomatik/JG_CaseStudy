using UnityEngine;

namespace Views
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private GameObject _wheelObject;
        [SerializeField] private int        _wheelSegments;
        [SerializeField] private int        _wheelStopValue;
        [SerializeField] private float      _wheelSpeed;

        private WheelView _wheel;

        public void Initialize()
        {
            _wheel = _wheelObject.GetComponent<WheelView>();
            
            _wheel.Initialize(_wheelSegments);
            _wheel.SetSpinDuration(_wheelSpeed);
        }
        
        public void SpinWheel()
        {
            _wheel.SetStopNumber(_wheelStopValue);
            _wheel.Spin();
        }
    }
}