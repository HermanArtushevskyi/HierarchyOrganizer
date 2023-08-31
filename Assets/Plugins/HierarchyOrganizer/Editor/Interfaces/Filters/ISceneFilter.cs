using UnityEngine;

namespace HierarchyOrganizer.Editor.Interfaces.Filters
{
	public interface ISceneFilter
	{
		public bool MeetsRequirements(GameObject go);
	}
}