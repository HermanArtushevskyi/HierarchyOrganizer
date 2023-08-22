using HierarchyOrganizer.Editor.Common;
using HierarchyOrganizer.Editor.Hierarchy.Conditions;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Factories;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.ScriptableObjectAdapters.Conditions
{
    [CreateAssetMenu(fileName = "LayerCondition", menuName = "HierarchyOrganizer/Conditions/Layer")]
    public class LayerConditionAdapter : ScriptableObject, IConditionFactory
    {
        [SerializeField] private SerializedLayerField _layer;
        [SerializeField] private LayerCondition.Mode _mode;
        
        public ICondition Create() => new LayerCondition(_mode, _layer.Value);
    }
}