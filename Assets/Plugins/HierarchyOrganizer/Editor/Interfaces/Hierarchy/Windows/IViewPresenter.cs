using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Interfaces.Hierarchy.Windows
{
	public interface IViewPresenter
	{
		public void Init(VisualElement root);
		public void Destroy();
	}
}