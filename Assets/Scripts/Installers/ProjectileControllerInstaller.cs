using CombatSystem.ProjectileSystem;
using Zenject;

namespace Installers
{
    public class ProjectileControllerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ProjectileController>().AsSingle();
        }
    }
}