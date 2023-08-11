using HierarchyOrganizer.Editor.Hierarchy.Restructurers;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Factories;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.ScriptableObjectAdapters.Restructures
{
	[CreateAssetMenu(fileName = "NameRestructure", menuName = "HierarchyOrganizer/Restructures/Name", order = 0)]
	public class NameRestructureAdapter : ScriptableObject, IRestructureFactory
	{
		[SerializeField] private string _value;
		[SerializeField] private NameRestructure.Mode _mode;
		
		public IRestructure Create() => new NameRestructure(_value, _mode);
	}
}