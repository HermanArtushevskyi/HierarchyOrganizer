using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.Conditions
{
	public class ComponentCondition : ConditionBase
	{
		public enum Mode
		{
			Has,
			Lacks,
			Prototype
		}

		public ComponentCondition(ComponentCondition.Mode mode, MonoBehaviour script)
		{
			
		}

		public override void Do(GameObject obj)
		{
			
		}

		public override void Undo(GameObject obj)
		{
		}
	}
}