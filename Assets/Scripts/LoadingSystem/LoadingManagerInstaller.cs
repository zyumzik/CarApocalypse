using Configurations;
using UnityEngine;
using Zenject;

namespace LoadingSystem
{
    public class LoadingManagerInstaller : MonoInstaller
    {
        [SerializeField] private ScenesConfiguration _scenesConfiguration;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LoadingManager>().AsSingle().WithArguments(_scenesConfiguration);
        }
    }
}