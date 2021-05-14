using System.Collections.Generic;
using System.Linq;
using Company.Project.Content;
using Company.Project.ContentData;
using Company.Project.Features.Inventory;
using Company.Project.Features.Items;
using Profile;
using Tools;
using UnityEngine;

namespace Company.Project.Features.Shed
{
    public class ShedController : BaseController, IShedController
    {
        private readonly Car _car;
        
        private readonly IUpgradable _upgradable;
        private readonly IRepository<int, IUpgradeHandler> _upgradeHandlersRepository;
        private readonly IInventoryController _inventoryController;
        private ProfilePlayer _profilePlayer;
        
        private List<UpgradeItemConfig> _upgradeItemsConfigCollection;

        #region Life cycle

        public ShedController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _upgradeHandlersRepository = CreateItemsConfigs();
            _inventoryController = CreateItemController(placeForUi);
            _upgradable = profilePlayer.CurrentCar;
            _profilePlayer = profilePlayer;
        }

        #endregion
        
        #region Methods
        
        private void UpgradeCarWithEquippedItems(
            IUpgradable upgradable,
            IReadOnlyList<IItem> equippedItems, 
            IReadOnlyDictionary<int, IUpgradeHandler> upgradeHandlers)
        {
            foreach (var equippedItem in equippedItems)
            {
                if (upgradeHandlers.TryGetValue(equippedItem.Id, out var handler))
                {
                    handler.Upgrade(upgradable);
                }
            }
        }

        private UpgradeHandlersRepository CreateItemsConfigs()
        {
            _upgradeItemsConfigCollection = ContentDataSourceLoader.LoadUpgradeItemConfigs(new ResourcePath
                    {PathResource = "DataSource/Upgrade/UpgradeItemConfigDataSource"});
            
            return new UpgradeHandlersRepository(_upgradeItemsConfigCollection);
        }

        private IInventoryController CreateItemController(Transform placeForUi)
        {
            var itemsRepository = new ItemsRepository(
                _upgradeItemsConfigCollection.Select(value => value.itemConfig).ToList());
            var inventoryModel = new InventoryModel();
            
            var inventoryViewPath = new ResourcePath {PathResource = $"Prefabs/{nameof(InventoryView)}"};
            var inventoryView = ResourceLoader.LoadAndInstantiateObject<InventoryView>(
                inventoryViewPath, placeForUi, false);
            
            AddGameObjects(inventoryView.gameObject);
            
            var inventoryController = new InventoryController(itemsRepository, inventoryModel, inventoryView);
            AddController(inventoryController);

            return inventoryController;
        }

        #endregion
        
        #region IShedController

        public void Enter()
        {
            _inventoryController.ShowInventory(Exit);
        }
        
        public void Exit()
        {
            UpgradeCarWithEquippedItems(
                _upgradable, _inventoryController.GetEquippedItems(), _upgradeHandlersRepository.Collection);
            Debug.Log(_profilePlayer.CurrentCar.Speed);
            _profilePlayer.CurrentState.Value = GameState.Start;
        }

        #endregion
    }
}