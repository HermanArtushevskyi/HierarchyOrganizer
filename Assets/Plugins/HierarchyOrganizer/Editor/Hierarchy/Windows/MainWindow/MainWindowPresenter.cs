using System;
using HierarchyOrganizer.Editor.Hierarchy.Windows.ConsoleWindow;
using HierarchyOrganizer.Editor.Hierarchy.Windows.GlobalGroupsWindow;
using HierarchyOrganizer.Editor.Hierarchy.Windows.LocalWindow;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Windows;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using SettingsProvider = HierarchyOrganizer.Editor.Settings.SettingsProvider;

namespace HierarchyOrganizer.Editor.Hierarchy.Windows.MainWindow
{
	public class MainWindowPresenter : EditorWindow, IViewPresenter
	{
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
			Init(AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(GetPath()).Instantiate());
		}

		public event Action OnDestroy;

		public void Init(VisualElement root)
		{
			rootVisualElement.Add(root);
			_root = root;
			_body = root.Q("body");

			root.Q<ToolbarButton>("globalBtn").clicked += () => SwitchPresenter(new GlobalViewPresenter());
			root.Q<ToolbarButton>("localBtn").clicked += () => SwitchPresenter(new LocalViewPresenter());
			root.Q<ToolbarButton>("issueBtn").clicked += () => SwitchPresenter(new ConsoleViewPresenter());
			
			SwitchPresenter(new GlobalViewPresenter());
		}
		
		private void SwitchPresenter(IViewPresenter presenter)
		{
			if (presenter.Equals(_currentPresenter)) return;
			
			_currentPresenter?.Destroy();
			_currentPresenter = presenter;
			presenter.Init(_body);
		}

		public void Destroy()
		{
			OnDestroy?.Invoke();
		}

		private string GetPath() => SettingsProvider.GetPluginPath() + "Editor/Hierarchy/Windows/MainWindow/UXML/MainWindowView.uxml";
	}
}