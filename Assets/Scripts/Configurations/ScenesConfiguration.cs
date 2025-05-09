using LoadingSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Configurations
{
    [CreateAssetMenu(fileName = "ScenesConfiguration", menuName = "Configurations/Scenes Configuration")]
    public class ScenesConfiguration : ScriptableObject
    {
        [SerializeField] private SceneLoadingData[] _scenesData;
        
        public SceneLoadingData[] ScenesData => _scenesData;
    }
}