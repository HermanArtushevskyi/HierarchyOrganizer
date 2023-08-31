using UnityEditor;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Settings
{
	public sealed class SettingsVariableString : SettingsVariableBase
	{
		private static string PATH = SettingsProvider.GetPluginPath() +
		                             "Editor/Settings/UXML/SettingsStringField.uxml";

		private TextField _textField;

		public SettingsVariableString(string name, string alias) : base(name, alias)
		{
		}

		public override void SetValue(object val) => _textField.value = (string) val;

		public override void SaveValue() => EditorPrefs.SetString(VariableName, _textField.value);

		protected override object GetCurrentVariable() => SettingsProvider.GetPluginPath();

		protected override void AddUxml(VisualElement list)
		{
			TemplateContainer el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(PATH).Instantiate();
			
			Label label = el.Q<Label>("variableName");
			label.text = VariableName;
			ApplyAlias(label);
			
			_textField = el.Q<TextField>("value");
			_textField.value = (string) GetCurrentVariable();
			
			list.Add(el);
		}
	}
}