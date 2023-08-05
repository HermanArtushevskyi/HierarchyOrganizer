using System;
using HierarchyOrganizer.Editor.Interfaces.Filters;
using UnityEditor;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Filters.UXMLAdapters
{
	public class ResultElementAdapter : IResultElementAdapter
	{
		private const string UXML_PATH =
			"Assets/Plugins/HierarchyOrganizer/Editor/Filters/UXML/GameObjectResultView.uxml";
		
		private TemplateContainer _el = null;
		private VisualElement _root = null;
		private IMGUIContainer _icon = null;
		private Label _name = null;
		private Label _tag = null;
		private Button _check = null;

		public event Action<ResultElementAdapter> OnDestroy;
		public void Init(VisualElement root)
		{
			_el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH).Instantiate();
			_root = root;
			_icon = _el.Q<IMGUIContainer>("icon");
			_name = _el.Q<Label>("name");
			_tag = _el.Q<Label>("tag");
			_check = _el.Q<Button>("check");
			
			root.Add(_el);
		}

		public void Destroy()
		{
		}

		public void DestroyWithoutNotification()
		{
		}
	}
}