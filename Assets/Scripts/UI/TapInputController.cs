using System;
using Core.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class TapInputController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IInput
    {
        private bool _isPointerDown;
        private float _screenWidth;
        
        public event Action<float> OnInput;
        
        private void Awake()
        {
            _screenWidth = Screen.width;
        }

        private void Update()
        {
            if (!_isPointerDown) return;
            
            var mouseX = Input.mousePosition.x;
            var inputX = mouseX / _screenWidth * 2f - 1f;
            
            OnInput?.Invoke(inputX);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPointerDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPointerDown = false;
        }
    }
}