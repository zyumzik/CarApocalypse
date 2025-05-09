using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RaceProgressView : ElementUI
    {
        [SerializeField] private Image _progressFillImage;
        [SerializeField] private TMP_Text _progressText;

        private void OnEnable()
        {
            SetProgress(0);
        }

        public void SetProgress(float progress)
        {
            _progressFillImage.fillAmount = progress;
            _progressText.text = (progress * 100).ToString("F1") + "%";
        }
    }
}