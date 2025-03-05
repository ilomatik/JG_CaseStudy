using UnityEngine;

namespace Views
{
    public class WheelSlotView : MonoBehaviour
    {
        [SerializeField] private int _value;
        
        private MeshRenderer _meshRenderer;
        
        public int Value => _value;
        
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
    }
}