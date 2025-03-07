using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Wheel Settings")]
        [SerializeField] private bool  _randomizeWheelStopValue;
        [SerializeField] private int   _wheelStopValue;
        [SerializeField] private float _wheelSpeed;
        
        [Header("Table Settings")]
        [SerializeField] private int   _tableSlotsCount;
        [SerializeField] private float _tableSlotSpacing;

        public bool  RandomizeWheelStopValue => _randomizeWheelStopValue;
        public int   WheelStopValue          => _wheelStopValue;
        public float WheelSpeed              => _wheelSpeed;
        
        public int   TableSlotsCount         => _tableSlotsCount;
        public float TableSlotSpacing        => _tableSlotSpacing;
    }
}