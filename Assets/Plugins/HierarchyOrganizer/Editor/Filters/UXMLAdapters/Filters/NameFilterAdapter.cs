using System;
using HierarchyOrganizer.Editor.Interfaces.Filters;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using SettingsProvider = HierarchyOrganizer.Editor.Settings.SettingsProvider;

namespace HierarchyOrganizer.Editor.Filters.UXMLAdapters
{
	public class NameFilterElementAdapter : FilterAdapterBase
	{
		private static readonly string _uxmlPath = SettingsProvider.GetPluginPath() + 
		                                           "Editor/Filters/UXML/Filters/NameFilterView.uxml";

		private EnumField _modeField;
		private TextField _textField;
		private Button _deleteButton;

		public override void Init(VisualElement root)
		{
			Element = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(_uxmlPath).Instantiate();
			
			Root = root;
			root.Add(Element);
			
			if (!IsInitiated) InitiateNew();
			else Reinitialize();
		}

		private void InitiateNew()
		{
			_modeField = Element.Q<EnumField>();
			_textField = Element.Q<TextField>();
			Element.Q<Button>().clicked += Destroy;

			_modeField.Init(FilterName.Mode.Is);
			_textField.value = "";

			IsInitiated = true;
		}

		private void Reinitialize()
		{
			EnumField elementEnum = Element.Q<EnumField>();
			elementEnum.Init(_modeField.value);
			_modeField = elementEnum;

			TextField elementText = Element.Q<TextField>();
			elementText.value = _textField.value;
			_textField = elementText;

			Element.Q<Button>().clicked += Destroy;
		}

		public override ISceneFilter GetFilter() => new FilterName(_textField.value, (FilterName.Mode) _modeField.value);

		public override bool ValidateGameObject(GameObject go)
		{
			return new FilterName(_textField.value, (FilterName.Mode) _modeField.value).MeetsRequirements(go);
		}
	}
}