using UnityEngine;

namespace Ui
{
    [CreateAssetMenu(fileName = "DOTweenData", menuName = "DOTweenData")]
    public sealed class DOTweenData : ScriptableObject
    {
        [Header("Game DoTween data")] [SerializeField]
        private string _shakeCameraOnLosePath = "ShakesDataOnLose";

        [Header("Main menu DoTween data")] [SerializeField]
        private string _scaleStartButtonPath = "ScaleDataStartButton";

        private ScaleData _scalerStartButton;
        private ShakeData _shakeOnLose;

        // private ScaleData ScaleStartButton
        // {
        //     get
        //     {
        //         if (_scalerStartButton == null)
        //         {
        //             _scalerStartButton = LoadObject<ScaleData>("DataSource/DOTween" + _scaleStartButtonPath);
        //         }
        //
        //         return _scalerStartButton;
        //     }
        // }
    }
}
