using BufferSystem;
using PlayerLogics;
using Zenject;

namespace UI.Presenters
{
    public class InputPresenter
    {
        public InputPresenter(string rootUIId, Player player, BufferManager bufferManager)
        {
            var rootUI = bufferManager.GetObject<RootUI>(rootUIId);
            player.TurretController.Initialize(rootUI.TapInputController);
        }
    }
}