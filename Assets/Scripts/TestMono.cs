using System;
using GameManagerModule;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class TestMono : MonoBehaviour
    {
        [Inject] private GameManager _gameManager;
        
        private void OnEnable()
        {
            _gameManager.OnGameReady += OnGameReady;
            _gameManager.OnGameStart += OnGameStart;
            _gameManager.OnGameEnd += OnGameEnd;
            _gameManager.OnGameWin += OnGameWin;
            _gameManager.OnGameLose += OnGameLose;
        }

        private void OnDisable()
        {
            _gameManager.OnGameReady -= OnGameReady;
            _gameManager.OnGameStart -= OnGameStart;
            _gameManager.OnGameEnd -= OnGameEnd;
            _gameManager.OnGameWin -= OnGameWin;
            _gameManager.OnGameLose -= OnGameLose;
        }

        private void OnGameReady()
        {
            Debug.Log("OnGameReady");
        }

        private void OnGameStart()
        {
             Debug.Log("OnGameStart");
        }
        
        private void OnGameEnd()
        {
            Debug.Log("OnGameEnd");
        }
        
        private void OnGameWin()
        {
            Debug.Log("OnGameWin");
        }
        
        private void OnGameLose()
        {
            Debug.Log("OnGameLose");
        }
    }
}