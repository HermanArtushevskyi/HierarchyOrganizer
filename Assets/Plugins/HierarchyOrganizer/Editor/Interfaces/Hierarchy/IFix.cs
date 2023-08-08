using UnityEngine;

namespace HierarchyOrganizer.Editor.Interfaces.Hierarchy
{
	public interface IFix
	{
		public void Do(GameObject obj);
		public void Undo(GameObject obj);
	}
}