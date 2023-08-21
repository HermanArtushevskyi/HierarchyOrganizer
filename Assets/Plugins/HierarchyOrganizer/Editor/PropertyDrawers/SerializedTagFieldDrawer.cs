using HierarchyOrganizer.Editor.Common;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using SettingsProvider = HierarchyOrganizer.Editor.Settings.SettingsProvider;

namespace HierarchyOrganizer.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(SerializedTagField), true)]
    public class SerializedTagFieldDrawer : PropertyDrawer
    {
        private static string UxmlPath = SettingsProvider.GetPluginPath() +
                                         "Editor/PropertyDrawers/UXML/SerializedTagFieldView.uxml";

        private SerializedProperty _valueProperty;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            _valueProperty = property.FindPropertyRelative("Value");

            TemplateContainer el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlPath).Instantiate();

            TagField tagField = el.Q<TagField>();

            tagField.value = _valueProperty.stringValue;

            tagField.RegisterValueChangedCallback(OnChange);
            
            return el;
        }

        private void OnChange(ChangeEvent<string> evt)
        {
            _valueProperty.stringValue = evt.newValue;
            _valueProperty.serializedObject.ApplyModifiedProperties();
            _valueProperty.serializedObject.Update();
        }
    }
}