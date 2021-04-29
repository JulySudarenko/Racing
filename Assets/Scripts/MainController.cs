using System.Linq;
using Company.Project.Content;
using Company.Project.ContentData;
using Company.Project.Features.Inventory;
using Company.Project.Features.Shed;
using Game;
using Profile;
using Tools;
using Ui;
using UnityEngine;

internal sealed class MainController : BaseController
{
    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private ShedController _shedController;
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
                var shedController = ConfigureShedController(_placeForUi, _profilePlayer);
                _mainMenuController?.Dispose();
                break;
            default:
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                _shedController?.Dispose();
                break;
        }
    }

    private BaseController ConfigureShedController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        var upgradeItemsConfigCollection
            = ContentDataSourceLoader.LoadUpgradeItemConfigs(new ResourcePath
                {PathResource = "DataSource/Upgrade/UpgradeItemConfigDataSource"});
        var upgradeItemsRepository
            = new UpgradeHandlersRepository(upgradeItemsConfigCollection);

        var itemsRepository = new ItemsRepository(
            upgradeItemsConfigCollection.Select(value => value.itemConfig).ToList());
        var inventoryModel = new InventoryModel();
        var inventoryViewPath = new ResourcePath {PathResource = $"Prefabs/{nameof(InventoryView)}"};
        var inventoryView = ResourceLoader.LoadAndInstantiateObject<InventoryView>(
            inventoryViewPath, placeForUi, false);
        AddGameObjects(inventoryView.gameObject);
        var inventoryController = new InventoryController(itemsRepository, inventoryModel, inventoryView);
        AddController(inventoryController);

        var shedController =
            new ShedController(upgradeItemsRepository, inventoryController, profilePlayer.CurrentCar);
        AddController(shedController);

        return shedController;
    }

    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _shedController?.Dispose();
        _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        base.OnDispose();
    }
}
