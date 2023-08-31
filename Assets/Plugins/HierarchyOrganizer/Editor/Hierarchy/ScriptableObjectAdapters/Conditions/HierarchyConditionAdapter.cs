using HierarchyOrganizer.Editor.Common;
using HierarchyOrganizer.Editor.Hierarchy.Conditions;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Factories;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.ScriptableObjectAdapters.Conditions
{
    [CreateAssetMenu(fileName = "HierarchyCondition", menuName = "HierarchyOrganizer/Conditions/Hierarchy", order = 0)]
    public class HierarchyConditionAdapter : ScriptableObject, IConditionFactory
    {
        [SerializeField] private string[] _hierarchy;
        [SerializeField] private bool _isRelative;
        [SerializeField] private HierarchyCondition.Mode _mode;
        
        public ICondition Create()
        {
            HierarchyTree tree = new HierarchyTree(_hierarchy, _isRelative);
            return new HierarchyCondition(tree, _mode);
        }
    }
}