using UnityEngine;

namespace Views
{
    public class ChipView : MonoBehaviour
    {
        [SerializeField] private int _value;
        
        public int Value => _value;
    }
}