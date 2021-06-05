using DG.Tweening;
using UnityEngine;

namespace DoTween.Configs
{
    [CreateAssetMenu(fileName = "Configs/ScaleData", menuName = "ScaleData")]
    public sealed class ScaleData : ScriptableObject
    {
        [SerializeField] private Ease _ease;
        [SerializeField, Range(0f, 5f)] private float _duration = 1.0f;
        [SerializeField] private Vector2 _scale = new Vector2(1.2f, 1.2f);
        [SerializeField] private LoopType _loopType;

        public Ease Easy => _ease;

        public LoopType LoopType => _loopType;

        public Vector2 Scale => _scale;

        public float Duration => _duration;
    }
}
