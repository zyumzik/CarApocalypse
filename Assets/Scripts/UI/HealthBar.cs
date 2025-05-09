using CombatSystem;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private RegularHealth _regularHealth;
        [SerializeField] private GameObject _mainObject;
        [SerializeField] private bool _showOnlyWhenDamaged = false;
        [SerializeField] private SlicedFilledImage _instantFill;
        [SerializeField] private SlicedFilledImage _delayedFill;
        [SerializeField] private float _delaySpeed = 0.5f;

        private Tween _delayedTween;
        
        private void LateUpdate()
        {
            var targetProgress = (float)_regularHealth.CurrentHealth / (float)_regularHealth.MaxHealth;

            if (_showOnlyWhenDamaged)
            {
                if (targetProgress == 1 && _mainObject.activeInHierarchy)
                    _mainObject.SetActive(false);
                
                if (targetProgress < 1 && !_mainObject.activeInHierarchy)
                    _mainObject.SetActive(true);
            }
            else if (!_mainObject.activeInHierarchy)
                _mainObject.SetActive(true);
            
            _instantFill.fillAmount = targetProgress;
            _delayedFill.fillAmount = 
                Mathf.Lerp(_delayedFill.fillAmount, targetProgress, _delaySpeed * Time.deltaTime);
        }
    }
}