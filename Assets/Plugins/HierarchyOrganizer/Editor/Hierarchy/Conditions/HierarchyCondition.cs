using HierarchyOrganizer.Editor.Common;
using HierarchyOrganizer.Editor.Common.Extensions;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.Conditions
{
	public class HierarchyCondition : ConditionBase
	{
		private HierarchyTree _value;

		public enum Mode
		{
			Is
		}

		public HierarchyCondition(HierarchyTree value, Mode mode)
		{
			_value = value;

			if (mode == Mode.Is) Condition = IsPredicate;
		}

		private bool IsPredicate(GameObject go)
		{
			if (!_value.IsRelative 
			    &&
				HierarchySceneUtils.GetRootByName(go.scene, _value.NodesStructure[0].Name) == null)
				return false;
			
			return go.IsObjectInHierarchy(_value);
		}
	}
}