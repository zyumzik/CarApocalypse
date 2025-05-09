using UnityEngine;

namespace UI
{
    public class RootUI : MonoBehaviour
    {
        [SerializeField] private InputController _inputController;
        [SerializeField] private TapInputController _tapInputController; 
        [SerializeField] private IntroUI _introUI;
        [SerializeField] private RaceProgressView _raceProgressView;
        [SerializeField] private EndGameUI _endGameUI;

        public InputController InputController => _inputController;
        public TapInputController TapInputController => _tapInputController;
        public IntroUI IntroUI => _introUI;
        public RaceProgressView RaceProgressView => _raceProgressView;
        public EndGameUI EndGameUI => _endGameUI;
    }
}