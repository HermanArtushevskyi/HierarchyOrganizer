using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Windows;
using UnityEditor;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Hierarchy.Windows.GlobalGroupsWindow
{
	public class GlobalViewPresenter : IViewPresenter
	{
		private const string UXML_PATH = "Assets/Plugins/HierarchyOrganizer/Editor/Hierarchy/Windows/GlobalGroupsWindow/UXML/GlobalView.uxml";
		
		private ScrollView _scrollView;
		
		private VisualElement _root;
		
		public void Init(VisualElement root)
		{
			TemplateContainer el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH).Instantiate();

			_root = root;
			root.Add(el);
			
			_scrollView = el.Q<ScrollView>();
		}
		
		public void Destroy()
		{
			_root.Clear();
		}
	}
}