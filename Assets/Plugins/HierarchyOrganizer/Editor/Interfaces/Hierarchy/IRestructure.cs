using UnityEngine;

namespace HierarchyOrganizer.Editor.Interfaces.Hierarchy
{
	public interface IRestructure
	{
		public void Do(GameObject obj);
		public void Undo(GameObject obj);
	}
}