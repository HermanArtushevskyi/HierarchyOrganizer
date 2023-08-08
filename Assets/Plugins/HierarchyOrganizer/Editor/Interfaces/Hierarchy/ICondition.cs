using UnityEngine;

namespace HierarchyOrganizer.Editor.Interfaces.Hierarchy
{
	public interface ICondition : IFix
	{
		public bool IsMet(GameObject subject);
	}
}