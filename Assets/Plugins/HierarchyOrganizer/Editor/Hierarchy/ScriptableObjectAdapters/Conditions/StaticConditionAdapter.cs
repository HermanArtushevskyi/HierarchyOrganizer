using HierarchyOrganizer.Editor.Hierarchy.Conditions;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Factories;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.ScriptableObjectAdapters.Conditions
{
    [CreateAssetMenu(fileName = "Static condition", menuName = "HierarchyOrganizer/Conditions/Static")]
    public class StaticConditionAdapter : ScriptableObject, IConditionFactory
    {
        [SerializeField] private StaticCondition.Mode _mode;
        [SerializeField] private bool _value;
        
        public ICondition Create() => new StaticCondition(_mode, _value);
    }
}