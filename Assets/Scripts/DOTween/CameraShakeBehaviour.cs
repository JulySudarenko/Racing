using DG.Tweening;
using UnityEngine;

namespace DOTween
{
    public sealed class CameraShakeBehaviour
    {
        private ShakeData _data;
        private Transform _cameraTransform;

        public CameraShakeBehaviour(ShakeData data)
        {
            _data = data;
        }

        public void Init()
        {
            _cameraTransform = Camera.main.transform;
        }

        public void CreateShake()
        {
            Tweener tweener = DG.Tweening.DOTween.Shake(() => _cameraTransform.position, pos => _cameraTransform.position = pos,
                _data.Duration, _data.Strength, _data.Vibrato, _data.Randomness);
        }
    }
}
