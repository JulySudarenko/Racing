using Profile;
using Tools;
using UnityEngine;
using Game.CursorTrail;

using DG.Tweening;
using JoostenProductions;

namespace Ui
{
    internal sealed class MainMenuController : BaseController
    {
        #region Fields

        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;

        #endregion

        #region Life cycle

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = ResourceLoader.LoadAndInstantiateObject<MainMenuView>(
                new ResourcePath {PathResource = "Prefabs/mainMenu"}, placeForUi, false);
            AddGameObjects(_view.gameObject);
            _view.Init(StartGame);
            _view.InitShed(ShedEnter);
            _view.InitReward(RewardEnter);

            var cursorTrailController = ConfigureCursorTrail();
           //InitTweenButtons();
        }

        #endregion

        #region Methods

        private BaseController ConfigureCursorTrail()
        {
            var cursorTrailController = new CursorTrailController();
            AddController(cursorTrailController);
            return cursorTrailController;
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
        //
        // private void InitTweenButtons()
        // {
        //     var scaleStartButton = new ButtonScaleBehavior(_view.ButtonStart);
        //     scaleStartButton.ScaleButton();
        // }

        #endregion
    }
}
