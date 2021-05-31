using UnityEngine;

namespace DOTween
{
    [CreateAssetMenu(fileName = "Configs/DoTweenData", menuName = "DoTweenData")]
    public sealed class DoTweenData : ScriptableObject
    {
        [Header("Game DoTween data")] [SerializeField]
        private string _shakeCameraOnLosePath = "ShakesDataOnLose";

        [Header("Main menu DoTween data")] [SerializeField]
        private string _scaleStartButtonPath = "ScaleDataStartButton";

        private ScaleData _scalerStartButton;
        private ShakeData _shakeOnLose;

        public ScaleData ScaleStartButton
        {
            get
            {
                if (_scalerStartButton == null)
                {
                    _scalerStartButton = Resources.Load<ScaleData>("DataSource/DoTween" + _scaleStartButtonPath);
                }
        
                return _scalerStartButton;
            }
        }

        public ShakeData ShakeCameraOnLose
        {
            get
            {
                if (_shakeOnLose == null)
                {
                    _shakeOnLose = Resources.Load<ShakeData>("DataSource/DoTween" + _shakeCameraOnLosePath);
                }

                return _shakeOnLose;
            }
        }
    }
}
