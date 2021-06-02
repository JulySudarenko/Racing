using DG.Tweening;
using DoTween.Configs;
using Tools;
using UnityEngine;

namespace DoTween
{
    public sealed class CameraShakeBehaviour
    {
        private const string SHAKER_PATH = "DataSource/DoTween/ShakesDataOnLose";
        private readonly ShakeData _data;
        private readonly Transform _cameraTransform;
        private readonly Vector3 _startPosition;

        public CameraShakeBehaviour()
        {
            _cameraTransform = Camera.main.transform;
            _startPosition = _cameraTransform.position;
            var shakeCameraOnLosePath = new ResourcePath {PathResource = SHAKER_PATH};
            _data = ResourceLoader.LoadObject<ShakeData>(shakeCameraOnLosePath);
        }

        public void CreateShake()
        {
            Tweener tweener = DOTween.Shake(() => _cameraTransform.position,
                pos => _cameraTransform.position = pos, _data.Duration, _data.Strength, _data.Vibrato,
                _data.Randomness);
            _cameraTransform.position = _startPosition;
        }
    }
}
