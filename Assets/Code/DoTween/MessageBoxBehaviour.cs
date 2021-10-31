using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DoTween
{
    public sealed class MessageBoxBehaviour : MonoBehaviour
    {
        [SerializeField] private Button _buttonHide;
        [SerializeField] private Button _buttonExitGame;

        [SerializeField] private GameObject _root;
        [SerializeField] private Transform _message;
        [SerializeField] private Image _background;
        [SerializeField] private float _duration = 0.5f;


        public void InitMessageBox(UnityAction exit)
        {
            Hide(0);
            _buttonExitGame.onClick.AddListener(exit);
        }

        private void OnEnable()
        {
            _buttonHide.onClick.AddListener(ButtonHide_OnClick);
        }

        private void OnDisable()
        {
            _buttonHide.onClick.RemoveAllListeners();
            _buttonExitGame.onClick.RemoveAllListeners();
        }

        private void Show()
        {
            _root.SetActive(true);
            Sequence sequence =  DG.Tweening.DOTween.Sequence();
            sequence.Insert(0.0f, _background.DOFade(0.5f, _duration));
            sequence.Insert(0.0f, _message.DOScale(Vector3.one, _duration));
            sequence.OnComplete(() =>
            {
                sequence = null;
            });
        }

        private void Hide(float duration)
        {
            Sequence sequence =  DG.Tweening.DOTween.Sequence();
            sequence.Append(_message.DOScale(Vector3.zero, duration));
            sequence.Append(_background.DOFade(0.0f, duration));
            sequence.OnComplete(() =>
            {
                sequence = null;
                _root.SetActive(false);
            });
        }

        private void ButtonHide_OnClick()
        {
            Hide(_duration);
        }

        public void ButtonShow_OnClick()
        {
            Show();
        }
    }
}
