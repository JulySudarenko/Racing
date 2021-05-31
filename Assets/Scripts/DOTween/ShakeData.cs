using UnityEngine;

namespace DOTween
{
    [CreateAssetMenu(fileName = "Configs/ShakesData", menuName = "ShakesData")]
    public sealed class ShakeData : ScriptableObject
    {
        public float Duration;
        public float Strength;
        public int Vibrato;
        [Range(0.0f, 90.0f)] public float Randomness;
    }
}
