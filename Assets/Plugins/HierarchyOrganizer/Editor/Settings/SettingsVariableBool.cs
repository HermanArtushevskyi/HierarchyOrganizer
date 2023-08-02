using System;
using System.Reflection;
using UnityEditor;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Settings
{
	public sealed class SettingsVariableBool : SettingsVariableBase
	{
		private const string UXML_PATH =
			"Assets/Plugins/HierarchyOrganizer/Editor/Settings/UXML/SettingsBoolField.uxml";

		private bool _value;
		private Toggle _toggle;

		public SettingsVariableBool(string name, string alias, ScrollView list) : base(name, alias, list)
		{
		}

		public override void SetValue(object val) => _value = (bool) val;

		public override void SaveValue()
		{
			Type type = typeof(HierarchySettings);
			FieldInfo field = type.GetField(VariableName);
			field.SetValue(null, _toggle.value);
		}

		protected override void AddUxml(ScrollView list)
		{
			TemplateContainer el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH).Instantiate();
			
			Label label = el.Q<Label>("VariableName");
			label.text = VariableName;
			ApplyAlias(label);
			
			_toggle = el.Q<Toggle>("Toggle");
			_toggle.value = (bool) GetCurrentVariable();
			_value = _toggle.value;
			
			list.Add(el);
		}
	}
}