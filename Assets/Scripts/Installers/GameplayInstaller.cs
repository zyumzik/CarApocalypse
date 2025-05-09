using CameraModule;
using GameManagerModule;
using RoadSystem;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private RoadConstructor _roadConstructor;
        [SerializeField] private RoadRaceHandler _roadRaceHandler;
        [SerializeField] private GameManager _gameManager;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_cameraController).AsSingle();
            Container.BindInstance(_roadConstructor).AsSingle();
            Container.BindInstance(_roadRaceHandler).AsSingle();
            Container.BindInstance(_gameManager).AsSingle();
        }
        
    }
}