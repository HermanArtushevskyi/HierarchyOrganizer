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

		private object _userData;
		
		[MenuItem("LonelyStudio/HierarchyOrganizer/Find", priority = 1)]
		private static void ShowWindow()
		{
			var window = GetWindow<FiltersEditor>();
			window.titleContent = new GUIContent("Find");
			window.Show();
		}

		private void CreateGUI()
		{
			rootVisualElement.Add(AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH).Instantiate());
			_body = rootVisualElement.Q<VisualElement>("Body");

			rootVisualElement.Q<Button>("filtersBtn").clicked += () => SwitchAdapter(new FiltersBuilderViewAdapter());
			rootVisualElement.Q<Button>("resultsBtn").clicked += () => SwitchAdapter(null);
			
			_currentAdapter = SwitchAdapter(new FiltersBuilderViewAdapter());
		}

		private IViewAdapter SwitchAdapter(IViewAdapter adapter)
		{
			if (_currentAdapter != null && _currentAdapter.RequestUserData(out var userData)) _userData = userData;
			_currentAdapter?.Destroy();
			
			IViewAdapter view = adapter;
			view.Init(_body, _userData);
			
			return view;
		}
	}
}