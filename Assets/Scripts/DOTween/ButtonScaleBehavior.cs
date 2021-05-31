using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DOTween
{
    internal sealed class ButtonScaleBehavior : Button
    {
        [SerializeField] public ScaleData _scaleData;
        [SerializeField] private Vector2 _scaleUp = new Vector2(1.2f, 1.2f);
        private float _duration = 1.0f;
        
        public void ScaleButton()
        {
            Sequence sequence = DG.Tweening.DOTween.Sequence();
            sequence.Append(transform.DOScale(_scaleUp, _duration).SetEase(Ease.OutSine));
            sequence.SetLoops(-1, LoopType.Yoyo);
        }

        public void OnDestroy()
        {
            transform.DOKill();
        }
    }
}
