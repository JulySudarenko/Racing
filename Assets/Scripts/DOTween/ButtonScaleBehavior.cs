using DG.Tweening;
using DoTween.Configs;
using UnityEngine.UI;

namespace DoTween
{
    internal sealed class ButtonScaleBehavior : Button
    {
        public void ScaleButton(ScaleData data)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(data.Scale, data.Duration).SetEase(data.Easy));
            sequence.SetLoops(-1, data.LoopType);
        }

        protected override void OnDestroy()
        {
            transform.DOKill();
            base.OnDestroy();
        }
    }
}
