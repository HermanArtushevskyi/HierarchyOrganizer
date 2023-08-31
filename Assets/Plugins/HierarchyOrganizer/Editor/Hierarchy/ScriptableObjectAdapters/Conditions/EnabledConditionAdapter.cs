using HierarchyOrganizer.Editor.Hierarchy.Conditions;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Factories;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.ScriptableObjectAdapters.Conditions
{
    [CreateAssetMenu(fileName = "EnabledCondition", menuName = "HierarchyOrganizer/Conditions/Enabled", order = 0)]
    public class EnabledConditionAdapter : ScriptableObject, IConditionFactory
    {
        [SerializeField] private bool _value;
        [SerializeField] private EnabledCondition.Mode _mode;
        
        public ICondition Create() => new EnabledCondition(_value, _mode);
    }
}