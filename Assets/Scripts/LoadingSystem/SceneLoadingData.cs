using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LoadingSystem
{
    [Serializable]
    public class SceneLoadingData
    {
        [SerializeField] private AssetReference _sceneAssetReference;
        [SerializeField] private bool _isActive;

        public AssetReference SceneAssetReference => _sceneAssetReference;
        public bool IsActive => _isActive;
    }
}