using Configurations;
using PlayerLogics;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private PlayerConfiguration _playerConfiguration;
        
        public override void InstallBindings()
        {
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle()
                .WithArguments(_playerPrefab, _playerConfiguration);
            
            Container.Bind<Player>().FromMethod(context =>
            {
                var factory = context.Container.Resolve<IPlayerFactory>();
                return factory.Create(_spawnPoint.position, _spawnPoint.rotation);
            }).AsSingle().NonLazy();
        }
    }
}