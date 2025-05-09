using BufferSystem;
using Zenject;

namespace Installers
{
    public class BufferManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BufferManager>().AsSingle();
        }
    }
}