using UnityEngine;
using Views.BetAreas;

namespace Views.Chip
{
    public class ChipDragHandler : MonoBehaviour
    {
        private Vector3  _offset;
        private Vector3  _initialPosition;
        private bool     _isDragging = false;
        private Camera   _mainCamera;
        private ChipView _chipView;

        private void Start()
        {
            _mainCamera      = Camera.main;
            _initialPosition = transform.position;
            _chipView        = GetComponent<ChipView>();
        }

        private void OnMouseDown()
        {
            _offset = transform.position - GetMouseWorldPos();
            _isDragging = true;
        }

        private void OnMouseDrag()
        {
            if (_isDragging)
            {
                transform.position = GetMouseWorldPos() + _offset;
            }
        }

        private void OnMouseUp()
        {
            _isDragging = false;

            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider != null && hit.collider.GetComponent<BetArea>())
                {
                    BetArea betArea = hit.collider.GetComponent<BetArea>();
                    betArea.PlaceChip(_chipView);
                }
                else
                {
                    ReturnToStart();
                }
            }
            else
            {
                ReturnToStart();
            }
        }

        private Vector3 GetMouseWorldPos()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = _mainCamera.WorldToScreenPoint(transform.position).z;
            return _mainCamera.ScreenToWorldPoint(mousePos);
        }

        private void ReturnToStart()
        {
            transform.position = _initialPosition;
        }
    }
}