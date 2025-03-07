using Enums;
using UnityEngine;

namespace Views.BetAreas
{
    public class BetArea : MonoBehaviour
    {
        [SerializeField] private BetType _betType;
        
        public BetType BetType => _betType;
        
        public void ShowWinning()
        {
            Debug.Log($"{BetType} is winning!");
        }
        
        public void ShowLosing()
        {
            Debug.Log($"{BetType} is losing!");
        }
    }
}