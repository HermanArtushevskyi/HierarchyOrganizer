using System;
using HierarchyOrganizer.Editor.Interfaces.Filters;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Filters.UXMLAdapters
{
	public class NameFilterElementAdapter : ISceneFilterElementAdapter
	{
		private const string UXML_PATH = "Assets/Plugins/HierarchyOrganizer/Editor/Filters/UXML/Filters/NameFilterView.uxml";

		private VisualElement _root;
		private TemplateContainer _el;
		private EnumField _modeField;
		private TextField _textField;
		private Button _deleteButton;

		public event Action<NameFilterElementAdapter> OnDelete = null;

		public void Init(VisualElement root)
		{
			_el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH).Instantiate();
			
			_root = root;
			root.Add(_el);
			
			_modeField = _el.Q<EnumField>();
			_textField = _el.Q<TextField>();
			_el.Q<Button>().clicked += Destroy;

			_modeField.Init(FilterName.Mode.Is);
			_textField.value = "";
		}

		public ISceneFilter GetFilter() => new FilterName(_textField.value, (FilterName.Mode) _modeField.value);

		public bool ValidateGameObject(GameObject go)
		{
			return new FilterName(_textField.value, (FilterName.Mode) _modeField.value).MeetsRequirements(go);
		}

		public void Destroy()
		{
			DestroyWithoutNotification();
			OnDelete?.Invoke(this);
		}

		public void DestroyWithoutNotification() => _root.Remove(_el);
	}
}