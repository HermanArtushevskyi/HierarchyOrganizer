using HierarchyOrganizer.Editor.Hierarchy.Restructurers;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Factories;
using UnityEngine;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Hierarchy.ScriptableObjectAdapters.Restructures
{
	[CreateAssetMenu(fileName = "Component", menuName = "HierarchyOrganizer/Restructures/ComponentRestructure")]
	public class ComponentRestructureAdapter : ScriptableObject, IRestructureFactory
	{
		[SerializeReference] private DropdownField _script;
		[SerializeField] private ComponentRestructure.Mode _mode;

		public IRestructure Create() => new ComponentRestructure(_script.GetType(), _mode);
	}
}