using DG.Tweening;
using UnityEngine;

namespace Ui
{
    [CreateAssetMenu(fileName = "ScaleData", menuName = "ScaleData")]
    public sealed class ScaleData : ScriptableObject
    {
        public Ease Ease;
        [Range(0f, 5f)] public float Duration = 1.0f;
        public Vector2 Scale = new Vector2(1.2f, 1.2f);
        public LoopType LoopType;
    }
}
