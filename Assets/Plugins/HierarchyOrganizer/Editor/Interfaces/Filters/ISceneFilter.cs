using UnityEngine;

namespace Plugins.HierarchyOrganizer.Editor.Interfaces.Filters
{
	public interface ISceneFilter
	{
		public bool MeetsRequirements(GameObject go);
	}
}