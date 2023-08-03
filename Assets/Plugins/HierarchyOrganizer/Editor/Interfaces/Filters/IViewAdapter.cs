using UnityEngine.UIElements;

namespace Plugins.HierarchyOrganizer.Editor.Interfaces.Filters
{
	public interface IViewAdapter
	{
		public void Init(VisualElement root);
		public void Destroy();
	}
}