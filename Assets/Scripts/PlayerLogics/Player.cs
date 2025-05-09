using CombatSystem;
using Configurations;
using DetectionModule;
using UnityEngine;

namespace PlayerLogics
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private RegularHealth _regularHealth;
        [SerializeField] private CarMovement _carMovement;
        [SerializeField] private ProjectileLauncher _projectileLauncher;
        [SerializeField] private TurretController _turretController;
        [SerializeField] private Transform _cameraLookPoint;
        [SerializeField] private Transform _roadDetectorParent;
        
        public RegularHealth RegularHealth => _regularHealth;
        public TurretController TurretController => _turretController;
        public Transform CameraLookPoint => _cameraLookPoint;
        
        private PlayerConfiguration _playerConfiguration;
        
        public void Initialize(PlayerConfiguration playerConfiguration)
        {
            _playerConfiguration = playerConfiguration;

            _regularHealth.Initialize(_playerConfiguration.MaxHealth);
            _carMovement.Initialize(_playerConfiguration.MovementSpeed);
            _projectileLauncher.Initialize(_playerConfiguration.ShootInterval);
        }

        public void Respawn()
        {
            _regularHealth.Initialize(_playerConfiguration.MaxHealth);
        }

        public void AssignRoadDetector(RoadDetector detector)
        {
            detector.transform.SetParent(_roadDetectorParent, false);
            detector.transform.localPosition = Vector3.zero;
        }
    }
}