using System.Linq;
using HierarchyOrganizer.Editor.Common;
using UnityEditor;
using UnityEngine.UIElements;
using SettingsProvider = HierarchyOrganizer.Editor.Settings.SettingsProvider;

namespace HierarchyOrganizer.Editor.PropertyDrawers
{
	[CustomPropertyDrawer(typeof(MonoBehaviourReference), true)]
	public class MonoBehaviourReferenceDrawer : PropertyDrawer
	{
		private static string UxmlPath = SettingsProvider.GetPluginPath() +
		                                  "Editor/PropertyDrawers/UXML/MonoBehaviourReferenceView.uxml";

		private static string FieldName = "MonoBehaviourName";

		private SerializedProperty _property;
		
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			var el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlPath).Instantiate();

			string[] allMonoBehs = HierarchyProjectUtils.GetAllMonoBehavioursNamesInProjectAsync();

			DropdownField dropdown = el.Q<DropdownField>();
			dropdown.choices = allMonoBehs.ToList();

			_property = property.FindPropertyRelative(FieldName);

			dropdown.value = _property.stringValue;

			dropdown.RegisterValueChangedCallback(OnChange);
			
			return el;
		}

		private void OnChange(ChangeEvent<string> evt)
		{
			_property.stringValue = evt.newValue;
			_property.serializedObject.ApplyModifiedProperties();
			_property.serializedObject.Update();
		}
	}
}