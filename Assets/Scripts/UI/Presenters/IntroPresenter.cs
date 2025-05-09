using BufferSystem;
using GameManagerModule;

namespace UI.Presenters
{
    public class IntroPresenter
    {
        public IntroPresenter(string rootUIId, BufferManager bufferManager, GameManager gameManager)
        {
            var rootUI = bufferManager.GetObject<RootUI>(rootUIId);
            var introUI = rootUI.IntroUI;
            
            introUI.Initialize(gameManager.StartGame);
            gameManager.OnGameReady += introUI.Show;
            gameManager.OnGameStart += introUI.Hide;
        }
    }
}