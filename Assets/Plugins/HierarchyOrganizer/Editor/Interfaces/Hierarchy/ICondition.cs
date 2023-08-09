using UnityEngine;

namespace HierarchyOrganizer.Editor.Interfaces.Hierarchy
{
	public interface ICondition
	{
		public bool IsMet(GameObject subject);
	}
}