using System;
using System.Collections.Generic;
using Plugins.HierarchyOrganizer.Editor.Interfaces.Filters;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Filters.UXMLAdapters
{
	public class ResultsBuilderViewAdapter : IViewBuilderAdapter
	{
		private const string UXML_PATH =
			"Assets/Plugins/HierarchyOrganizer/Editor/Filters/UXML/ResultsBuilderView.uxml";
		
		private List<ISceneFilter> _appliedFilters;
		private List<IResultElementAdapter> _resultAdapters;

		private TemplateContainer _el = null;
		private VisualElement _root = null;

		public event Action<ResultsBuilderViewAdapter> OnDestroy;

		public void Init(VisualElement root)
		{
			Debug.LogError("Can not initiate ResultsBuilderViewAdapter without user data");
		}

		public void Init(VisualElement root, object userData)
		{
			_el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH).Instantiate();
			_root = root;
			
			root.Add(_el);

			_appliedFilters = (List<ISceneFilter>) userData;
		}

		public bool RequestUserData(out object userData)
		{
			userData = null;
			return false;
		}

		public void Destroy()
		{
			DestroyWithoutNotification();
			OnDestroy?.Invoke(this);
		}

		public void DestroyWithoutNotification()
		{
			_root.Clear();
		}
	}
}