using UnityEngine.UIElements;

namespace Plugins.HierarchyOrganizer.Editor.Interfaces.Filters
{
	public interface IViewAdapter
	{
		public void Init(VisualElement root, object userData);
		public bool RequestUserData(out object userData);
		public void Destroy();
	}
}