using System;
using Profile;
using UnityEngine;
using Game.CursorTrail;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Ui
{
    internal sealed class MainMenuController : BaseController
    {
        #region Fields

        private readonly ProfilePlayer _profilePlayer;
        private MainMenuView _view;
        private AsyncOperationHandle<GameObject> _mainMenuLoaded;
        private Transform _placeForUi;

        #endregion

        #region Life cycle

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, AssetReference mainMenuPrefab)
        {
            _profilePlayer = profilePlayer;
            _placeForUi = placeForUi;
            mainMenuPrefab.LoadAssetAsync<GameObject>().Completed += OnCompleted;
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

        private void InitMainMenu(AsyncOperationHandle<GameObject> obj)
        {
            _mainMenuLoaded = obj;
            var view = _mainMenuLoaded.Result;
            view.transform.SetParent(_placeForUi);
            view.transform.localScale = Vector3.one;
            view.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            view.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            _view = view.GetComponent<MainMenuView>();
            AddGameObjects(_view.gameObject);
            InitButtons();

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
        
        protected override void OnDispose()
        {
            Addressables.Release(_mainMenuLoaded);
        }

        #endregion

        private void OnCompleted(AsyncOperationHandle<GameObject> obj)
        {
            switch (obj.Status)
            {
                case AsyncOperationStatus.None:
                    break;
                case AsyncOperationStatus.Succeeded:
                    InitMainMenu(obj);
                    break;
                case AsyncOperationStatus.Failed:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
