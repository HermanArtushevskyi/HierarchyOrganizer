using System;
using HierarchyOrganizer.Editor.Hierarchy.Windows.GlobalGroupsWindow;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Windows;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Hierarchy.Windows.MainWindow
{
	public class MainWindowPresenter : EditorWindow, IViewPresenter
	{
		private const string UXML_PATH = "Assets/Plugins/HierarchyOrganizer/Editor/Hierarchy/Windows/MainWindow/UXML/MainWindowView.uxml";

		private VisualElement _root = null;
		private VisualElement _body = null;

		private IViewPresenter _currentPresenter;
		
		[MenuItem("LonelyStudio/HierarchyOrganizer/Groups")]
		private static void ShowWindow()
		{
			var window = GetWindow<MainWindowPresenter>();
			window.titleContent = new GUIContent("Groups");
			window.Show();
		}

		private void CreateGUI()
		{
			Init(AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH).Instantiate());
		}

		public void Init(VisualElement root)
		{
			rootVisualElement.Add(root);
			_root = root;
			
			_body = root.Q("body");

			root.Q<ToolbarButton>("globalBtn").clicked += () => SwitchPresenter(new GlobalViewPresenter());
			root.Q<ToolbarButton>("localBtn").clicked += () => throw new NotImplementedException();
			
			SwitchPresenter(new GlobalViewPresenter());
		}
		
		public void Destroy()
		{
		}

		private void SwitchPresenter(IViewPresenter presenter)
		{
			if (presenter == _currentPresenter) return;
			
			_currentPresenter?.Destroy();
			presenter.Init(_body);
			_currentPresenter = presenter;
		}
	}
}