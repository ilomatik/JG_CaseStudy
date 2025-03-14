using System.Collections.Generic;
using System.Linq;
using Constants;
using Enums;
using Events;
using UnityEngine;
using Views.BetAreas;
using Views.Interfaces;

namespace Views.Chip
{
    public class ChipDragHandler : MonoBehaviour
    {
        private Vector3  _offset;
        private Vector3  _initialPosition;
        private bool     _isDragging = false;
        private Camera   _mainCamera;
        private ChipView _chipView;
        private BetArea  _betArea;

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
            _chipView.transform.localEulerAngles = new Vector3(0, 0, 0);
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

            if (!DetectBetArea())
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.2f);
                int detectedBetAreasCount = 0;

                foreach (Collider col in hitColliders)
                {
                    BetArea betArea = col.GetComponent<BetArea>();
                
                    if (betArea != null)
                    {
                        detectedBetAreasCount++;
                    }
                }

                if (detectedBetAreasCount is 0 or > 1)
                {
                    ReturnToStart();
                }
                else
                {
                    BetArea betArea = hitColliders.First(x => x.GetComponent<BetArea>() != null).GetComponent<BetArea>();
                    betArea.PlaceChip(_chipView);
                    _betArea = betArea;
                    
                    SendEvents();
                }
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
            _chipView.transform.localEulerAngles = new Vector3(90, 0, 0);
            
            if (_chipView.IsOnBetArea)
            {
                GameEvents.RemoveChip(_chipView);

                if (_betArea != null)
                {
                    BetEvents.RemoveBet(PlayerPrefs.GetInt(GameConstant.PLAYER_ID_COUNTER),
                                        _betArea.BetType, 
                                        _chipView.GetSlotNumbers(),
                                        _chipView.ChipType);
                }
            }
        }
        
        private bool DetectBetArea()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.2f);
            List<int> detectedNumbers = new List<int>();

            foreach (Collider col in hitColliders)
            {
                ITableSlotView slot = col.GetComponent<ITableSlotView>();
                
                if (slot != null)
                {
                    detectedNumbers.Add(slot.SlotNumber);
                }
            }
            
            int detectedSlotsCount = detectedNumbers.Count;

            if (detectedSlotsCount > 0)
            {
                BetType betType = BetType.None;
                
                if (detectedSlotsCount == 1)
                {
                    betType = BetType.Single;
                }
                else if (detectedSlotsCount == 2)
                {
                    betType = BetType.Split;
                }
                else if (detectedSlotsCount == 4)
                {
                    betType = BetType.Corner;
                }
                
                _chipView.SetOnBetArea(true);
                _chipView.SetSlotNumbers(detectedNumbers);
                _chipView.transform.localEulerAngles = new Vector3(0, 0, 0);
                
                SendEvents();
                
                BetEvents.PlaceBet(PlayerPrefs.GetInt(GameConstant.PLAYER_ID_COUNTER), 
                                   betType, 
                                   detectedNumbers, 
                                   _chipView.ChipType);
                
                return true;
            }
            else
            {
                Debug.Log("Could not hit slot!");
                
                return false;
            }
        }

        private void SendEvents()
        {
            GameEvents    .PlaceChip(gameObject);
            AudioEvents   .PlayChipDrop();
            ParticleEvents.DropChipParticle(transform.position);
        }
    }
}