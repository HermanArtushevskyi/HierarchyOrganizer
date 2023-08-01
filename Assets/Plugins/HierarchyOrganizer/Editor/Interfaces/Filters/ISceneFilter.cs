using UnityEngine;

namespace Plugins.HierarchyOrganizer.Editor.Interfaces.Filters
{
	public interface ISceneFilter
	{
		public GameObject[] GetObjects();
		public GameObject GetObject();
	}
}