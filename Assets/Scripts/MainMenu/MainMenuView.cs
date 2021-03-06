using DoTween;
using DoTween.Configs;
using Tools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public sealed class MainMenuView : MonoBehaviour
    {
        [SerializeField] private MessageBoxBehaviour _messageBox;
        [SerializeField] private ButtonScaleBehavior _buttonStart;
        [SerializeField] private Button _buttonShed;
        [SerializeField] private Button _buttonReward;
        [SerializeField] private Button _buttonExit;
        
        private const string PATH = "DataSource/DoTween/ScaleDataStartButton";
        private ScaleData _data;
        
        public void InitStartGame(UnityAction startGame)
        {
            _buttonStart.onClick.AddListener(startGame);
            var scaleStartButtonPath = new ResourcePath {PathResource = PATH};
            _data = ResourceLoader.LoadObject<ScaleData>(scaleStartButtonPath);
            _buttonStart.ScaleButton(_data);
        }

        public void InitShed(UnityAction shedEnter)
        {
            _buttonShed.onClick.AddListener(shedEnter);
        }

        public void InitReward(UnityAction rewardEnter)
        {
            _buttonReward.onClick.AddListener(rewardEnter);
        }

        public void InitExit(UnityAction exit)
        {
            _messageBox.InitMessageBox(exit);
            _buttonExit.onClick.AddListener(_messageBox.ButtonShow_OnClick);
        }

        private void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonShed.onClick.RemoveAllListeners();
            _buttonReward.onClick.RemoveAllListeners();
            _buttonExit.onClick.RemoveAllListeners();
        }
    }
}

