using HierarchyOrganizer.Editor.Common;
using HierarchyOrganizer.Editor.Hierarchy.Restructurers;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Factories;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.ScriptableObjectAdapters.Restructures
{
    [CreateAssetMenu(fileName = "TagRestructure", menuName = "HierarchyOrganizer/Restructures/Tag", order = 0)]
    public class TagRestructureAdapter : ScriptableObject, IRestructureFactory
    {
        [SerializeField] private SerializedTagField _tag;
        [SerializeField] private TagRestructure.Mode _mode;
        
        public IRestructure Create() => new TagRestructure(_tag.Value, _mode);
    }
}