using Configurations;
using UnityEngine;
using Zenject;

namespace PlayerLogics
{
    public class PlayerFactory : IPlayerFactory
    {
        private DiContainer _container;
        private readonly Player _playerPrefab;
        private readonly PlayerConfiguration _playerConfiguration;

        public PlayerFactory(Player playerPrefab, PlayerConfiguration playerConfiguration)
        {
            //_container = container;
            _playerPrefab = playerPrefab;
            _playerConfiguration = playerConfiguration;
        }

        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }
        
        public Player Create(Vector3 position, Quaternion rotation)
        {
            var playerGo = _container.InstantiatePrefab(_playerPrefab, position, rotation, null);
            var player = playerGo.GetComponent<Player>();
            player.Initialize(_playerConfiguration);
            
            return player;
        }
    }
}