using UnityEngine.UIElements;

namespace Plugins.HierarchyOrganizer.Editor.Interfaces.Filters
{
	public interface IVisualElementAdapter
	{
		public void Init(VisualElement root);
		public void Destroy();
		public void DestroyWithoutNotification();
		
	}
}