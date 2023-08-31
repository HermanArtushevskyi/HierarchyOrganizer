using HierarchyOrganizer.Editor.Hierarchy.Restructurers;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Factories;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.ScriptableObjectAdapters.Restructures
{
    [CreateAssetMenu(fileName = "EnabledRestructure", menuName = "HierarchyOrganizer/Restructures/Enabled", order = 0)]
    public class EnabledRestructureAdapter : ScriptableObject, IRestructureFactory
    {
        [SerializeField] private bool _value;
        [SerializeField] private EnabledRestructure.Mode _mode;


        public IRestructure Create() => new EnabledRestructure(_value, _mode);
    }
}