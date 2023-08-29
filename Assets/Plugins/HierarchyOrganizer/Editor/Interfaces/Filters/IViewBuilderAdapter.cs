using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Interfaces.Filters
{
	public interface IViewBuilderAdapter : IVisualElementAdapter
	{
		public void Init(VisualElement root, object userData, object savedData);
		public bool RequestUserData(out object userData);
        public bool SaveUserData(out object savedData);


    }
}