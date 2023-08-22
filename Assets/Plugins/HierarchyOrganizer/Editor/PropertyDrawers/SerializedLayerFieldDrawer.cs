using HierarchyOrganizer.Editor.Common;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using SettingsProvider = HierarchyOrganizer.Editor.Settings.SettingsProvider;

namespace HierarchyOrganizer.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(SerializedLayerField))]
    public class SerializedLayerFieldDrawer : PropertyDrawer
    {
        private static string UxmlPath = SettingsProvider.GetPluginPath() +
                                         "Editor/PropertyDrawers/UXML/SerializedLayerFieldView.uxml";

        private SerializedProperty _valueProperty;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            TemplateContainer el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlPath).Instantiate();
            
            _valueProperty = property.FindPropertyRelative("Value");

            LayerField layerField = el.Q<LayerField>();

            layerField.value = _valueProperty.intValue;
            
            layerField.RegisterValueChangedCallback(OnChange);

            return el;
        }

        private void OnChange(ChangeEvent<int> evt)
        {
            _valueProperty.intValue = evt.newValue;
            _valueProperty.serializedObject.ApplyModifiedProperties();
            _valueProperty.serializedObject.Update();
        }
    }
}