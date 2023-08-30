﻿using System;
using System.Collections.Generic;
using System.Linq;
using HierarchyOrganizer.Editor.Interfaces.Filters;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


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
      //  private readonly List<ISceneFilterElementAdapter> _savedFilters = new List<ISceneFilterElementAdapter>();



        public FiltersBuilderViewBuilderAdapter()
		{
			FilterToFunc = new Dictionary<AvailableFilter, Action<ScrollView>>
			{
				{AvailableFilter.Tag, AddTagFilter},

                {AvailableFilter.Component, AddComponentFilter},

                {AvailableFilter.Name, AddNameFilter}

            };
		}

		public void Init(VisualElement root)
		{
			Init(root, null);

		}

		public void Init(VisualElement root, object userData)
		{
			_root = root;
			AddUXML(root);
			RegisterButtons();
			if (userData != null) _addedFilters.AddRange((List<ISceneFilterElementAdapter>)userData);
             
            
			
            GetAdapter();


        }

        public bool RequestUserData(out object userData)
        {
            
          userData = _addedFilters;
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
			_enumField.Init(AvailableFilter.Component);
			_scrollView = _el.Q<ScrollView>();
			_addButton = _el.Q<Button>("addBtn");
			_clearButton = _el.Q<Button>("clearBtn");
		}

     

        private void GetAdapter()
        {

            foreach (ISceneFilterElementAdapter adapter in _addedFilters)
			{
				adapter.Init(_scrollView);
			}

        }

		




        private void RegisterButtons()
		{

            _addButton.clicked += () => FilterToFunc[(AvailableFilter)_enumField.value].Invoke(_scrollView);
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