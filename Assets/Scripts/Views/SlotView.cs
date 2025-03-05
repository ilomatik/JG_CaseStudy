using UnityEngine;

namespace Views
{
    public class SlotView : MonoBehaviour
    {
        [SerializeField] private int _slotValue;
        
        private MeshRenderer _meshRenderer;

        public int SlotValue => _slotValue;
        
        public void Initialize()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }
        
        public void ChangeWinningColor()
        {
            _meshRenderer.material.color = Color.green;
        }
        
        public void ChangeLosingColor()
        {
            _meshRenderer.material.color = Color.red;
        }
        
        public void ChangeDefaultColor()
        {
            _meshRenderer.material.color = Color.gray;
        }

        public void ShowWinning()
        {
            Debug.Log($"Slot{_slotValue} is winning!");
        }
    }
}