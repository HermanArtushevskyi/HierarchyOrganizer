using System;
using HierarchyOrganizer.Editor.Interfaces.Filters;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Filters.UXMLAdapters
{
	public class ResultElementAdapter : IResultElementAdapter
	{
		private const string UXML_PATH =
			"Assets/Plugins/HierarchyOrganizer/Editor/Filters/UXML/GameObjectResultView.uxml";

		private const string IconPath = "Assets/Plugins/HierarchyOrganizer/Graphics/GameObjectIcon.png";

		private static Sprite _iconSprite = null;
		
		private TemplateContainer _el = null;
		private VisualElement _root = null;
		private Label _name = null;
		private Label _tag = null;
		private Button _check = null;

		public event Action<ResultElementAdapter> OnDestroy;

		public void Init(VisualElement root) => Init(root, null);

		public void Init(VisualElement root, GameObject data)
		{
			_el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH).Instantiate();
			_root = root;

			_name = _el.Q<Label>("name");
			_name.text = data.name;
			
			_tag = _el.Q<Label>("tag");
			_tag.text = data.tag;
			
			_check = _el.Q<Button>("check");
			_check.clicked += () => Selection.activeObject = data;

			root.Add(_el);
		}

		public void Destroy()
		{
			DestroyWithoutNotification();
			OnDestroy?.Invoke(this);
		}

		public void DestroyWithoutNotification()
		{
			_root.Remove(_el);
		}

	}
}