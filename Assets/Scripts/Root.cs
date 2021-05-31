using DOTween;
using Profile;
using Profile.Analytic;
using UnityEngine;

internal sealed class Root : MonoBehaviour
{
    [SerializeField] private DoTweenData _doTweenData;
    [SerializeField] private Transform _placeForUi;
    [SerializeField] private float _speedCar = 15.0f;
    [SerializeField] private float _forceCar = 15.0f;
    [SerializeField] private int _crimeRate = 4;
    
    private MainController _mainController;

    private void Awake()
    {
        ProfilePlayer profilePlayer = new ProfilePlayer(_speedCar, _forceCar, _crimeRate, new UnityAnalyticTools());
        profilePlayer.CurrentState.Value = GameState.Start;
        _mainController = new MainController(_placeForUi, profilePlayer, _doTweenData);
    }

    private void OnDestroy()
    {
        _mainController?.Dispose();
    }
}
