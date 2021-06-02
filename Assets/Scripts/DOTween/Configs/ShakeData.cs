using UnityEngine;

namespace DoTween.Configs
{
    [CreateAssetMenu(fileName = "Configs/ShakesData", menuName = "ShakesData")]
    public sealed class ShakeData : ScriptableObject
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _strength;
        [SerializeField] private int _vibrato;
        [SerializeField, Range(0.0f, 90.0f)] private float _randomness;

        public float Duration => _duration;

        public float Strength => _strength;

        public float Randomness => _randomness;

        public int Vibrato => _vibrato;
    }
}
