using UnityEngine;
using UnityEngine.UI;

namespace Company.Project.Content
{
    [CreateAssetMenu(fileName = "Configs/Item", menuName = "Item", order = 0)]
    public class ItemConfig : ScriptableObject
    {
        public int id;
        public string title;
        public Image icon;
    }
}