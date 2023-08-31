using HierarchyOrganizer.Editor.Common;
using HierarchyOrganizer.Editor.Hierarchy.Restructurers;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Factories;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.ScriptableObjectAdapters.Restructures
{
    [CreateAssetMenu(fileName = "LayerRestructure", menuName = "HierarchyOrganizer/Restructures/Layer", order = 0)]
    public class LayerRestructureAdapter : ScriptableObject, IRestructureFactory
    {
        [SerializeField] private SerializedLayerField _layer;
        [SerializeField] private LayerRestructure.Mode _mode;
        
        public IRestructure Create() => new LayerRestructure(_layer.Value, _mode);
    }
}