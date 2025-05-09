using System;
using Core.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class InputController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IInput
    {
        [SerializeField] private RectTransform _dragArea;
        [SerializeField] private float _sensitivity = 10f;
        
        private Camera _mainCamera;
        private Vector2 _startPosition;
        private Vector2 _endPosition;
        
        public event Action<float> OnInput; 

        public void Initialize(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _dragArea, eventData.position, _mainCamera, out var localPoint);
            
            _startPosition = localPoint;
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _dragArea, eventData.position, _mainCamera, out var localPoint);
            
            _endPosition = localPoint;
            
            var delta = Mathf.Clamp(_endPosition.x - _startPosition.x, -_sensitivity, _sensitivity);
            delta /= _sensitivity;
            OnInput?.Invoke(delta);
            
            Debug.Log($"Delta: {delta}");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _startPosition = Vector2.zero;
        }

    }
}