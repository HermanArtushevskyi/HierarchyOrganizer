using UnityEngine;
using UnityEngine.UIElements;

namespace Plugins.HierarchyOrganizer.Editor.Interfaces.Filters
{
	public interface ISceneFilterAdapter
	{
		public void Init(VisualElement root);
		public ISceneFilter GetFilter();
		public bool ValidateGameObject(GameObject go);
		public void Destroy();
		public void DestroyWithoutNotification();
	}
}