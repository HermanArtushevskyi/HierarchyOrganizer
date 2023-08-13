﻿using System;
using System.Collections.Generic;
using HierarchyOrganizer.Editor.Hierarchy.Data;
using HierarchyOrganizer.Editor.Hierarchy.Windows.Common.GroupVariable;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Windows;
using UnityEditor;
using UnityEngine.UIElements;

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
			_globalService = GlobalGroupsData.GetInstance();
			
			TemplateContainer el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH).Instantiate();

			_root = root;
			root.Add(el);
			
			_scrollView = el.Q<ScrollView>();
			_addBtn = el.Q<Button>("addBtn");
			_clearBtn = el.Q<Button>("clearBtn");
			_saveBtn = el.Q<Button>("saveBtn");

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
				foreach (GroupVariableAdapter adapter in _groupAdapters)
				{
					_globalService.GlobalGroups.Clear();
					_globalService.GlobalGroups.Add(adapter.GetData() as IGroup);
				}
				
				_globalService.SaveData();
			};

			LoadData();
		}

		private void DeleteFromList(GroupVariableAdapter obj) => _groupAdapters.Remove(obj);

		public void Destroy()
		{
			if (_root != null) _root.Clear();
			OnDestroy?.Invoke();
		}

		private void LoadData()
		{
		}
	}
}