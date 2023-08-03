using HierarchyOrganizer.Editor.Filters.UXMLAdapters;
using Plugins.HierarchyOrganizer.Editor.Interfaces.Filters;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Filters
{
	public class FiltersEditor : EditorWindow
	{
		private const string UXML_PATH = "Assets/Plugins/HierarchyOrganizer/Editor/Filters/UXML/SceneFiltersView.uxml";

		private VisualElement _body;
		private IViewAdapter _currentAdapter;

		[MenuItem("LonelyStudio/HierarchyOrganizer/Find", priority = 1)]
		private static void ShowWindow()
		{
			var window = GetWindow<FiltersEditor>();
			window.titleContent = new GUIContent("Find on scene");
			window.Show();
		}

		private void CreateGUI()
		{
			rootVisualElement.Add(AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH).Instantiate());
			_body = rootVisualElement.Q<VisualElement>("Body");

			_currentAdapter = CreateFiltersBuilderView();
		}

		private IViewAdapter CreateFiltersBuilderView()
		{
			_currentAdapter?.Destroy();
			FiltersBuilderViewAdapter view = new FiltersBuilderViewAdapter();
			view.Init(_body);
			return view;
		}
	}
}