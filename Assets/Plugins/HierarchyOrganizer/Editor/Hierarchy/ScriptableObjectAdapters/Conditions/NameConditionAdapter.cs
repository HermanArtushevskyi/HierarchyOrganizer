using HierarchyOrganizer.Editor.Hierarchy.Conditions;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Factories;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.ScriptableObjectAdapters.Conditions
{
	[CreateAssetMenu(fileName = "NameCondition", menuName = "HierarchyOrganizer/Conditions/Name", order = 0)]
	public class NameConditionAdapter : ScriptableObject, IConditionFactory
	{
		public string Value;
		public NameCondition.Mode Mode;
		
		public ICondition Create()
		{
			return new NameCondition(Mode, Value);
		}
	}
}