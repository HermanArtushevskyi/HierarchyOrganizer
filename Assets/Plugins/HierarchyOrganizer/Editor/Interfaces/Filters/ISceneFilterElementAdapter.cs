using UnityEngine;

namespace HierarchyOrganizer.Editor.Interfaces.Filters
{
	public interface ISceneFilterElementAdapter : IVisualElementAdapter
	{
		public ISceneFilter GetFilter();
      
        public bool ValidateGameObject(GameObject go);
	}
}