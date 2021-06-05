using UnityEngine.UI;

namespace Company.Project.Features.Items
{
    public interface IItem
    {
        int Id { get; }
        ItemInfo Info { get; }
        Image Icon { get; }
    }

    public struct ItemInfo
    {
        public string Title { get; set; }
    }
}