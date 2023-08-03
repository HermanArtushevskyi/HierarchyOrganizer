using System;
using Plugins.HierarchyOrganizer.Editor.Interfaces.Filters;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Filters.UXMLAdapters
{
	public class TagFilterAdapter : ISceneFilterAdapter
	{
		private const string UXML_PATH = "Assets/Plugins/HierarchyOrganizer/Editor/Filters/UXML/Filters/TagFilterView.uxml";

		private TemplateContainer _el;
		private EnumField _modeField;
		private TextField _textField;
		private Button _deleteButton;

		public event Action OnDelete = null;

		public void Init(VisualElement root)
		{
			_el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH).Instantiate();
			root.Add(_el);
			
			_modeField = _el.Q<EnumField>();
			_textField = _el.Q<TextField>();
			_el.Q<Button>().clicked += () => DeleteMyself(root);

			_modeField.Init(FilterTag.Mode.Is);
			_textField.value = "";
		}

		public ISceneFilter GetFilter() => new FilterTag(_textField.value, (FilterTag.Mode) _modeField.value);

		public bool ValidateGameObject(GameObject go)
		{
			return new FilterTag(_textField.value, (FilterTag.Mode) _modeField.value).MeetsRequirements(go);
		}

		private void DeleteMyself(VisualElement root)
		{
			root.Remove(_el);
			OnDelete.Invoke();
		}
	}
}