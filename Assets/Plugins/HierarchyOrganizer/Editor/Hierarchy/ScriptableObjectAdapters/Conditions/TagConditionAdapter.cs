using System;
using HierarchyOrganizer.Editor.Hierarchy.Conditions;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Factories;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.ScriptableObjectAdapters.Conditions
{
	[CreateAssetMenu(fileName = "TagCondition", menuName = "HierarchyOrganizer/Conditions/Tag", order = 0)]
	public class TagConditionAdapter : ScriptableObject, IConditionFactory
	{
		public string Value;
		public TagCondition.Mode Mode;
		
		public ICondition Create() => new TagCondition(Mode, Value);
	}
}