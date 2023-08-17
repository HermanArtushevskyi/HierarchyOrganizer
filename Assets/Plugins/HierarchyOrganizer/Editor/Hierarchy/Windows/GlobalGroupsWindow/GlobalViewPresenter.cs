using System;
using System.Collections.Generic;
using System.Linq;
using HierarchyOrganizer.Editor.Hierarchy.Data;
using HierarchyOrganizer.Editor.Hierarchy.Groups;
using HierarchyOrganizer.Editor.Hierarchy.Windows.Common.GroupVariable;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Windows;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace HierarchyOrganizer.Editor.Hierarchy.Windows.GlobalGroupsWindow
{
	public class GlobalViewPresenter : IViewPresenter
	{
		private const string UXML_PATH = "Assets/Plugins/HierarchyOrganizer/Editor/Hierarchy/Windows/GlobalGroupsWindow/UXML/GlobalView.uxml";
		
		private VisualElement _root;
		private ScrollView _scrollView;
		private Button _addBtn;
		private Button _clearBtn;
		private Button _saveBtn;

		private GlobalGroupsData _globalService = null;

		private List<GroupVariableAdapter> _groupAdapters = new List<GroupVariableAdapter>();

		public event Action OnDestroy;

		public void Init(VisualElement root)
		{
			TemplateContainer el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH).Instantiate();

			_root = root;
			root.Add(el);
			
			_scrollView = el.Q<ScrollView>();
			_addBtn = el.Q<Button>("addBtn");
			_clearBtn = el.Q<Button>("clearBtn");
			_saveBtn = el.Q<Button>("saveBtn");

			LoadData();

			_addBtn.clicked += () =>
			{
				GroupVariableAdapter adapter = new GroupVariableAdapter();
				_groupAdapters.Add(adapter);
				adapter.Init(_scrollView);
				adapter.OnDestroyThis += DeleteFromList;
			};

			_clearBtn.clicked += () =>
			{
				foreach (GroupVariableAdapter adapter in _groupAdapters) adapter.DestroyWithoutNotification();
				_groupAdapters.Clear();
			};

			
			_saveBtn.clicked += () =>
			{
				List<string> _groupsGUID = new();
				
				foreach (GroupVariableAdapter adapter in _groupAdapters)
				{
					string guid;
					long lid;
					AssetDatabase.TryGetGUIDAndLocalFileIdentifier((Object) adapter.GetData(), out guid, out lid);
					_groupsGUID.Add(guid);
				}

				_globalService.SetGlobalGroups(_groupsGUID.ToArray());
				EditorUtility.SetDirty(_globalService);
				AssetDatabase.SaveAssetIfDirty(_globalService);
				AssetDatabase.Refresh();
			};
		}

		private void DeleteFromList(GroupVariableAdapter obj) => _groupAdapters.Remove(obj);

		public void Destroy()
		{
			if (_root != null) _root.Clear();
			OnDestroy?.Invoke();
		}

		private void LoadData()
		{
			_globalService = AssetDatabase.LoadAssetAtPath<GlobalGroupsData>(GlobalGroupsData.PATH);

			if (_globalService == null)
			{
				Debug.LogWarning("Global data is not found");
				return;
			} 

			if (_globalService.GlobalGroupsGUID == null) return;

			string[] allGroupObjects = AssetDatabase.FindAssets("t:GroupScriptableObject");
			
			foreach (string guid in _globalService.GlobalGroupsGUID)
			{
				if (_globalService.GlobalGroupsGUID.Contains(guid))
				{
					GroupScriptableObject group =
						AssetDatabase.LoadAssetAtPath<GroupScriptableObject>(AssetDatabase.GUIDToAssetPath(guid));
					
					GroupVariableAdapter adapter = new GroupVariableAdapter();
					_groupAdapters.Add(adapter);
					adapter.Init(_scrollView, group);
					adapter.OnDestroyThis += DeleteFromList;
				}
			}
		}
	}
}