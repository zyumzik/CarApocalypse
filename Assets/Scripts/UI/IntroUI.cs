using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class IntroUI : ElementUI
    {
        [SerializeField] private Button _startGameButton;

        private Action _onStartButton;
        
        public void Initialize(Action onStartButton)
        {
            _onStartButton = onStartButton;
        }

        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(StartButtonClicked);
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(StartButtonClicked);
        }

        private void StartButtonClicked()
        {
            _onStartButton?.Invoke();
        }
    }
}