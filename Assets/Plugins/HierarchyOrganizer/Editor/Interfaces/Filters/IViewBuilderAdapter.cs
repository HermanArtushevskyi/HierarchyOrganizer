using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Interfaces.Filters
{
	public interface IViewBuilderAdapter : IVisualElementAdapter
	{
		public void Init(VisualElement root, object userData);
		public bool RequestUserData(out object userData);
		
      



    }
}