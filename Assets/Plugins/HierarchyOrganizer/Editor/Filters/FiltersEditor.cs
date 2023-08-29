using HierarchyOrganizer.Editor.Filters.UXMLAdapters;
using HierarchyOrganizer.Editor.Interfaces.Filters;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Filters
{
	public class FiltersEditor : EditorWindow
	{
		private const string UXML_PATH = "Assets/Plugins/HierarchyOrganizer/Editor/Filters/UXML/SceneFiltersView.uxml";

		private VisualElement _body;
		private IViewBuilderAdapter _currentBuilderAdapter;

		private object _userData;
        private object _savedData;



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

			rootVisualElement.Q<Button>("filtersBtn").clicked += () => SwitchAdapter(new FiltersBuilderViewBuilderAdapter());
			rootVisualElement.Q<Button>("resultsBtn").clicked += () => SwitchAdapter(new ResultsBuilderViewAdapter()); 
			
			_currentBuilderAdapter = SwitchAdapter(new FiltersBuilderViewBuilderAdapter()); 
		}

		private IViewBuilderAdapter SwitchAdapter(IViewBuilderAdapter builderAdapter) 
		{
			if (_currentBuilderAdapter == builderAdapter) return _currentBuilderAdapter;
			if (_currentBuilderAdapter != null && _currentBuilderAdapter.RequestUserData(out var userData)&& _currentBuilderAdapter.SaveUserData(out var savedData))
			{
				_userData = userData;
				_savedData = savedData;
			}
			
			
				
			

            _currentBuilderAdapter?.Destroy();
			
			IViewBuilderAdapter viewBuilder = builderAdapter;
	
			viewBuilder.Init(_body, _userData, _savedData);

			_currentBuilderAdapter = builderAdapter;
			
			return viewBuilder;
		}
	}
}