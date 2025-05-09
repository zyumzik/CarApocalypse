using System;
using BufferSystem;
using GameManagerModule;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Object = UnityEngine.Object;

namespace UI
{
    public class EndGameUI : ElementUI
    {
        [SerializeField] private Button _victoryButton;
        [SerializeField] private Button _lostButton;
        [SerializeField] private TMP_Text _endText;
        [SerializeField] private string _victoryText = "You won!";
        [SerializeField] private string _lostText = "You lost, but don't give up!";

        private Action _onRestartButton;
        
        public void Initialize(Action onRestartButton)
        {
            _onRestartButton = onRestartButton;
        }

        private void OnEnable()
        {
            _victoryButton.onClick.AddListener(OnButtonClick);
            _lostButton.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _victoryButton.onClick.RemoveListener(OnButtonClick);
            _lostButton.onClick.RemoveListener(OnButtonClick);
        }

        public void ShowGameResult(bool victory)
        {
            _endText.text = victory ? _victoryText : _lostText;
            _victoryButton.gameObject.SetActive(victory);
            _lostButton.gameObject.SetActive(!victory);
            
            Show();
        }
        
        private void OnButtonClick()
        {
            _onRestartButton?.Invoke();
            Hide();
        }
    }
}