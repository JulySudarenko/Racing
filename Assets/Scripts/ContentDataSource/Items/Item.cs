using Company.Project.Features.Items;
using UnityEngine.UI;

namespace Company.Project.Content
{
    public class Item : IItem
    {
        public int Id { get; set; }
        public ItemInfo Info { get; set; }
        public Image Icon { get; set; }
    }
}