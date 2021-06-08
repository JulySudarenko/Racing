using Company.Project.Features.Shed;
using DoTween.Configs;
using Game;
using Profile;
using Rewards;
using Ui;
using UnityEngine;

internal sealed class MainController : BaseController
{
    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private ShedController _shedController;
    private RewardController _rewardController;
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    public MainController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;
        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                _gameController?.Dispose();
                _shedController?.Dispose();
                break;
            case GameState.Game:
                _gameController = new GameController(_placeForUi, _profilePlayer);
                _mainMenuController?.Dispose();
                break;
            case GameState.Shed:
                _shedController = new ShedController(_placeForUi, _profilePlayer);
                _mainMenuController?.Dispose();
                break;
            case GameState.Reward:
                _rewardController = new RewardController();
                _mainMenuController?.Dispose();
                break;
            case GameState.Exit:
                Application.Quit();
                break;
            default:
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                _shedController?.Dispose();
                _rewardController?.Dispose();
                break;
        }
    }

    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _shedController?.Dispose();
        _rewardController?.Dispose();
        _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        base.OnDispose();
    }
}
