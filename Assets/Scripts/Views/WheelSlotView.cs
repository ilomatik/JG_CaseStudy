using UnityEngine;

namespace Views
{
    public class WheelSlotView : MonoBehaviour
    {
        [SerializeField] private int _value;
        
        public int Value => _value;
    }
}