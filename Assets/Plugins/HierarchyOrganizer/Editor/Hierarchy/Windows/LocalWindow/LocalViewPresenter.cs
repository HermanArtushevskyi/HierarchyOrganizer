using System;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Windows;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Hierarchy.Windows.LocalWindow
{
	public class LocalViewPresenter : IViewPresenter
	{
		private const string UXML_PATH =
			"Assets/Plugins/HierarchyOrganizer/Editor/Hierarchy/Windows/LocalWindow/UXML/LocalWindowView.uxml";
		
		private TemplateContainer _el;
		private VisualElement _root;
		
		public event Action OnDestroy;
		
		public void Init(VisualElement root)
		{
			_el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH).Instantiate();
			
			_root = root;
			
			root.Add(_el);

			_el.Q<ObjectField>().objectType = typeof(SceneAsset);
		}
		
		public void Destroy()
		{
			_root.Remove(_el);
		}
	}
}