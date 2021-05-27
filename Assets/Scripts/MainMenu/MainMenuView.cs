using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public sealed class MainMenuView : MonoBehaviour
    {
        [SerializeField] private ButtonScaleBehavior _buttonStart;
        [SerializeField] private Button _buttonShed;
        [SerializeField] private Button _buttonReward;
        [SerializeField] private Button _buttonExit;

        public void Init(UnityAction startGame)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonStart.ScaleButton();
        }

        public void InitShed(UnityAction shedEnter)
        {
            _buttonShed.onClick.AddListener(shedEnter);
        }

        public void InitReward(UnityAction rewardEnter)
        {
            _buttonReward.onClick.AddListener(rewardEnter);
        }

        private void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonShed.onClick.RemoveAllListeners();
            _buttonReward.onClick.RemoveAllListeners();
        }
    }

    internal class AppExit
    {
        public void Exit()
        {
            Application.Quit();
        }
    }
}

