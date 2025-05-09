using Configurations;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

namespace LoadingSystem
{
    public class LoadingManager : IInitializable
    {
        [Inject] private ScenesConfiguration _scenesConfiguration;

        private bool _isBaseScenesLoading;
        private bool _isBaseScenesLoaded;
        
        public void Initialize()
        {
             LoadBaseScenes();
        }

        private async void LoadBaseScenes()
        {
            if (_scenesConfiguration is null || _isBaseScenesLoading || _isBaseScenesLoaded) return;
            
            _isBaseScenesLoading = true;
            foreach (var sceneData in _scenesConfiguration.ScenesData)
            {
                var sceneInstance = await sceneData.SceneAssetReference.LoadSceneAsync(LoadSceneMode.Additive);
                if (sceneData.IsActive) SceneManager.SetActiveScene(sceneInstance.Scene);
            }
            
            _isBaseScenesLoading = false;
            _isBaseScenesLoaded = true;
        }
    }
}