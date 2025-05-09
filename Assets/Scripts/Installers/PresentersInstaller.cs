using UI.Presenters;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PresentersInstaller : MonoInstaller
    {
        [SerializeField] private string _rootUIId = "RootUI";
        
        public override void InstallBindings()
        {
            Container.Bind<InputPresenter>().AsSingle().WithArguments(_rootUIId).NonLazy();
            Container.Bind<IntroPresenter>().AsSingle().WithArguments(_rootUIId).NonLazy();
            Container.BindInterfacesAndSelfTo<RaceProgressPresenter>().AsSingle().WithArguments(_rootUIId).NonLazy();
            Container.Bind<EndGamePresenter>().AsSingle().WithArguments(_rootUIId).NonLazy();
        }
    }
}