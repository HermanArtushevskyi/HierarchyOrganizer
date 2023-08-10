using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.Restructurers
{
	public abstract class RestructureBase : ScriptableObject, IRestructure
	{
		public abstract void Do(GameObject obj);

		public abstract void Undo(GameObject obj);
	}
}