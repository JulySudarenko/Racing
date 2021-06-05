using UnityEngine;

namespace Company.Project.Content
{
    [CreateAssetMenu(fileName = "Configs/AbilityItemConfigDataSource", menuName = "AbilityItemConfigDataSource", order = 0)]
    public class AbilityItemConfigDataSource : ScriptableObject
    {
        public AbilityItemConfig[] itemConfigs;
    }
}