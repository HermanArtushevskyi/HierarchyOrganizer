using System;
using System.Collections.Generic;
using Plugins.HierarchyOrganizer.Editor.Interfaces.Filters;
using UnityEditor;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Filters.UXMLAdapters
{
	public partial class FiltersBuilderViewBuilderAdapter : IViewBuilderAdapter
	{
		private const string UXML_PATH =
			"Assets/Plugins/HierarchyOrganizer/Editor/Filters/UXML/FiltersBuilderView.uxml";

		private Dictionary<AvailableFilter, Action<ScrollView>> FilterToFunc;

		#region UXML elements
		private VisualElement _root = null;
		private TemplateContainer _el = null;
		private EnumField _enumField = null;
		private ScrollView _scrollView = null;
		private Button _addButton = null;
		private Button _clearButton = null;
		#endregion

		public event Action<FiltersBuilderViewBuilderAdapter> OnDestroy;

		private readonly List<ISceneFilterElementAdapter> _addedFilters = new List<ISceneFilterElementAdapter>();

		public FiltersBuilderViewBuilderAdapter()
		{
			FilterToFunc = new Dictionary<AvailableFilter, Action<ScrollView>>
			{
				{AvailableFilter.Tag, AddTagFilter}
			};
		}

		public void Init(VisualElement root)
		{
			Init(root, null);
		}

		public void Init(VisualElement root, object _)
		{
			_root = root;
			AddUXML(root);
			RegisterButtons();
		}

		public bool RequestUserData(out object userData)
		{
			List<ISceneFilter> filters = new();
			foreach (ISceneFilterElementAdapter adapter in _addedFilters) filters.Add(adapter.GetFilter());
			userData = filters;
			return true;
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

		private void AddUXML(VisualElement root)
		{
			_el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH).Instantiate();
			root.Add(_el);
			_enumField = _el.Q<EnumField>();
			_enumField.Init(AvailableFilter.Tag);
			_scrollView = _el.Q<ScrollView>();
			_addButton = _el.Q<Button>("addBtn");
			_clearButton = _el.Q<Button>("clearBtn");
		}

		private void RegisterButtons()
		{
			_addButton.clicked += () => FilterToFunc[(AvailableFilter) _enumField.value].Invoke(_scrollView);
			_clearButton.clicked += () =>
			{
				foreach (ISceneFilterElementAdapter adapter in _addedFilters)
				{
					adapter.DestroyWithoutNotification();
				}
				_addedFilters.Clear();
			};
		}
	}
}