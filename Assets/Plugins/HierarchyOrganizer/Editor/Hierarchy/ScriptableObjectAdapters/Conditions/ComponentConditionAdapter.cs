using HierarchyOrganizer.Editor.Hierarchy.Conditions;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Factories;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.ScriptableObjectAdapters.Conditions
{
	[CreateAssetMenu(fileName = "ComponentCondition", menuName = "HierarchyOrganizer/Conditions/Component")]
	public class ComponentConditionAdapter : ScriptableObject, IConditionFactory
	{
		public MonoBehaviour Value;
		public ComponentCondition.Mode Mode;
		
		public ICondition Create()
		{
			return new ComponentCondition(Mode, Value.GetType());
		}
	}
}