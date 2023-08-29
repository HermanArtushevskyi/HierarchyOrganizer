using System;
using System.Collections.Generic;
using System.Linq;
using HierarchyOrganizer.Editor.Interfaces.Filters;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


namespace HierarchyOrganizer.Editor.Filters.UXMLAdapters
{
	public class ResultsBuilderViewAdapter : IViewBuilderAdapter
	{
		private const string UXML_PATH =
			"Assets/Plugins/HierarchyOrganizer/Editor/Filters/UXML/ResultsBuilderView.uxml";
		
		private List<ISceneFilterElementAdapter> _appliedFilters;

		private TemplateContainer _el = null;
		private VisualElement _root = null;
		private VisualElement _body = null;

		public event Action<ResultsBuilderViewAdapter> OnDestroy;

		public void Init(VisualElement root)
		{
			Debug.LogError("Can not initiate ResultsBuilderViewAdapter without user data");
		}

		public void Init(VisualElement root, object userData, object Data)
		{
			_el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH).Instantiate();
			_root = root;
			_body = _el.Q("body");
			
			root.Add(_el);

			_appliedFilters = (List<ISceneFilterElementAdapter>) userData;

			GameObject[] gameobjects = SceneManager.GetActiveScene().GetRootGameObjects();
			ProcessGameObjects(gameobjects);
		}

		public bool RequestUserData(out object userData)
		{	
			userData = null;
			return false;
		}

        public bool SaveUserData(out object savedData)
        {
            savedData = null;
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

		private void ProcessGameObjects(GameObject[] gameobjects)
		{
			foreach (GameObject obj in gameobjects)
			{
				if (ProcessAllFilters(obj)) AddAdapter(obj);

				foreach (Transform component in obj.GetComponentsInChildren<Transform>())
				{
					if (component.gameObject == obj) continue;
					if (ProcessAllFilters(component.gameObject)) AddAdapter(component.gameObject);
				}
			}
		}

		private void AddAdapter(GameObject data)
		{
			ResultElementAdapter adapter = new ResultElementAdapter();
			adapter.Init(_body, data);
		}

		private bool ProcessAllFilters(GameObject data)
		{
			foreach (ISceneFilterElementAdapter filter in _appliedFilters)
			{
				if (!filter.ValidateGameObject(data)) return false;
			}

			return true;
		}
	}
}