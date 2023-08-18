using UnityEditor;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Settings
{
	public sealed class SettingsVariableBool : SettingsVariableBase
	{
		private static string UXML_PATH = SettingsProvider.GetPluginPath() + 
		                                  "Editor/Settings/UXML/SettingsBoolField.uxml";

		private Toggle _toggle;

		public SettingsVariableBool(string name, string alias) : base(name, alias)
		{
		}

		public override void SetValue(object val) => _toggle.value = (bool) val;

		public override void SaveValue() => EditorPrefs.SetBool(VariableName, _toggle.value);

		protected override object GetCurrentVariable() => EditorPrefs.GetBool(VariableName);
		
		protected override void AddUxml(VisualElement list)
		{
			TemplateContainer el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH).Instantiate();
			
			Label label = el.Q<Label>("VariableName");
			label.text = VariableName;
			ApplyAlias(label);
			
			_toggle = el.Q<Toggle>("Toggle");
			_toggle.value = (bool) GetCurrentVariable();
			
			list.Add(el);
		}
	}
}