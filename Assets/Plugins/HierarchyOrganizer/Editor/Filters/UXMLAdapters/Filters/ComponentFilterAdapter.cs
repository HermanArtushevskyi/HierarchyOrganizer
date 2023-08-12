using System;
using HierarchyOrganizer.Editor.Interfaces.Filters;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Filters.UXMLAdapters
{
    public class ComponentFilterElementAdapter : ISceneFilterElementAdapter
    {
        private const string UXML_PATH = "Assets/Plugins/HierarchyOrganizer/Editor/Filters/UXML/Filters/ComponentFilterView.uxml";

        private VisualElement _root;
        private TemplateContainer _el;
        private EnumField _modeField;
        private TextField _textField;
        private Button _deleteButton;

        public event Action<ComponentFilterElementAdapter> OnDelete = null;

        public void Init(VisualElement root)
        {
            _el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH).Instantiate();

            _root = root;
            root.Add(_el);

            _modeField = _el.Q<EnumField>();
            _textField = _el.Q<TextField>();
            _el.Q<Button>().clicked += Destroy;

            _modeField.Init(FilterComponent.Mode.Is);
            _textField.value = "";
        }
        


        public ISceneFilter GetFilter() => new FilterComponent(_textField.value, (FilterComponent.Mode)_modeField.value);
        
        public bool ValidateGameObject(GameObject go)
        {
            return new FilterComponent(_textField.value, (FilterComponent.Mode)_modeField.value).MeetsRequirements(go);
        }

        public void Destroy()
        {
            DestroyWithoutNotification();
            OnDelete?.Invoke(this);
        }

        public void DestroyWithoutNotification() => _root.Remove(_el);
    }
}