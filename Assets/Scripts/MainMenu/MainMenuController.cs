using DOTween;
using Profile;
using Tools;
using UnityEngine;
using Game.CursorTrail;

namespace Ui
{
    internal sealed class MainMenuController : BaseController
    {
        #region Fields

        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;
        private readonly ScaleData _scaleData;

        #endregion

        #region Life cycle

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, ScaleData scaleData)
        {
            _profilePlayer = profilePlayer;
            _scaleData = scaleData;
            _view = ResourceLoader.LoadAndInstantiateObject<MainMenuView>(
                new ResourcePath {PathResource = "Prefabs/mainMenu"}, placeForUi, false);
            AddGameObjects(_view.gameObject);
            InitButtons();
            
            var cursorTrailController = ConfigureCursorTrail();

        }

        #endregion

        #region Methods

        private BaseController ConfigureCursorTrail()
        {
            var cursorTrailController = new CursorTrailController();
            AddController(cursorTrailController);
            return cursorTrailController;
        }

        private void InitButtons()
        {
            _view.InitStartGame(StartGame);
            _view.InitShed(ShedEnter);
            _view.InitReward(RewardEnter);
            _view.InitExit(ExitGame);
        }
        
        private void StartGame()
        {
            _profilePlayer.CurrentState.Value = GameState.Game;
            _profilePlayer.AnalyticTools.SendMessage("start game");
        }

        private void ShedEnter()
        {
            _profilePlayer.CurrentState.Value = GameState.Shed;
            _profilePlayer.AnalyticTools.SendMessage("enter shed");
        }

        private void RewardEnter()
        {
            _profilePlayer.CurrentState.Value = GameState.Reward;
            _profilePlayer.AnalyticTools.SendMessage("get reward");
        }
        
        private void ExitGame()
        {
            _profilePlayer.CurrentState.Value = GameState.Exit;
        }

        #endregion
    }
}
