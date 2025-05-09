using TimerService;
using Zenject;

namespace Installers
{
    public class TimerServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ITimerService>().To<TimerService.TimerService>().AsSingle();
        }
    }
}