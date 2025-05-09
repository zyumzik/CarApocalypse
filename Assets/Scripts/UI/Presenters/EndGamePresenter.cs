using BufferSystem;
using GameManagerModule;

namespace UI.Presenters
{
    public class EndGamePresenter
    {
        public EndGamePresenter(string rootUIId, BufferManager bufferManager, GameManager gameManager)
        {
            var rootUI = bufferManager.GetObject<RootUI>(rootUIId);
            var endGameUI = rootUI.EndGameUI;
            
            endGameUI.Initialize(gameManager.RestartGame);

            gameManager.OnGameWin += () => { endGameUI.ShowGameResult(true); };
            gameManager.OnGameLose += () => { endGameUI.ShowGameResult(false); };
        }
    }
}