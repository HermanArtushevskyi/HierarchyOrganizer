using System;
using System.Collections.Generic;
using HierarchyOrganizer.Editor.Hierarchy.Data;
using HierarchyOrganizer.Editor.Hierarchy.ScriptableObjectAdapters.Groups;
using HierarchyOrganizer.Editor.Hierarchy.Windows.Common.GroupVariable;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Windows;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;
using SettingsProvider = HierarchyOrganizer.Editor.Settings.SettingsProvider;

namespace HierarchyOrganizer.Editor.Hierarchy.Windows.LocalWindow
{
	public class LocalViewPresenter : IViewPresenter
	{
		private string UXML_PATH = SettingsProvider.GetPluginPath() + 
		                                  "Editor/Hierarchy/Windows/LocalWindow/UXML/LocalWindowView.uxml";

		private TemplateContainer _el;
		private VisualElement _root;
		private ObjectField _objectField;
		private ScrollView _scrollView;

		private List<GroupVariableAdapter> _groupFields = new();

		public event Action OnDestroy;

		public void Init(VisualElement root)
		{
			_el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH).Instantiate();

			_root = root;

			root.Add(_el);

			_scrollView = _el.Q<ScrollView>();

			_objectField = _el.Q<ObjectField>();
			_objectField.objectType = typeof(SceneAsset);

			_objectField.RegisterValueChangedCallback(OnSceneChange);
			
			_el.Q<Button>("addBtn").clicked += Add;
			_el.Q<Button>("clearBtn").clicked += Clear;
			_el.Q<Button>("saveBtn").clicked += Save;
		}

		private void Add()
		{
			AddAdapter(_scrollView);
		}

		private void Clear()
		{
			foreach (GroupVariableAdapter adapter in _groupFields)
			{
				adapter.DestroyWithoutNotification();
			}
			_groupFields.Clear();
		}

		private void Save()
		{
			List<string> _groupsGUID = new();
				
			foreach (GroupVariableAdapter adapter in _groupFields)
			{
				string guid;
				long lid;
				AssetDatabase.TryGetGUIDAndLocalFileIdentifier((Object) adapter.GetData(), out guid, out lid);
				_groupsGUID.Add(guid);
			}
			
			SceneGroupsData.SetSceneGroups(((SceneAsset) _objectField.value).name, _groupsGUID.ToArray());
		}

		private void OnSceneChange(ChangeEvent<Object> evt)
		{
			SceneAsset newValue = (SceneAsset) evt.newValue;
			RefreshGroups(newValue);
		}

		private void RefreshGroups(SceneAsset sceneAsset)
		{
			_scrollView.Clear();
			IGroup[] groups = SceneGroupsData.GetSceneGroups(sceneAsset.name);

			foreach (IGroup group in groups) AddAdapter(_scrollView, group);
		}

		private void AddAdapter(VisualElement root, IGroup group)
		{
			GroupVariableAdapter adapter = new GroupVariableAdapter();
			adapter.Init(root, (GroupScriptableObject) group);
			_groupFields.Add(adapter);
			adapter.OnDestroyThis += DestroyAdapter;
		}
		
		private void AddAdapter(VisualElement root)
		{
			GroupVariableAdapter adapter = new GroupVariableAdapter();
			adapter.Init(root);
			_groupFields.Add(adapter);
			adapter.OnDestroyThis += DestroyAdapter;
		}

		private void DestroyAdapter(GroupVariableAdapter obj) => _groupFields.Remove(obj);

		public void Destroy()
		{
			_root.Remove(_el);
		}
	}
}