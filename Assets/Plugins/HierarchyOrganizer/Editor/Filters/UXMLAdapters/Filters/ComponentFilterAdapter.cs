using System;
using System.Collections.Generic;
using HierarchyOrganizer.Editor.Interfaces.Filters;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Filters.UXMLAdapters
{
    public class ComponentFilterElementAdapter : ISceneFilterElementAdapter
    {
        private const string UXML_PATH = "Assets/Plugins/HierarchyOrganizer/Editor/Filters/UXML/Filters/ComponentFilterView.uxml";

        private VisualElement _root;
        private TemplateContainer _el;
        private EnumField _modeField;
        private DropdownField _dropField;
        private Button _deleteButton;

        public event Action<ComponentFilterElementAdapter> OnDelete = null;

        public void Init(VisualElement root)
        {
            _el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH).Instantiate();

            _root = root;
            root.Add(_el);

            _modeField = _el.Q<EnumField>();
            _dropField = _el.Q<DropdownField>();
            _el.Q<Button>().clicked += Destroy;

            _modeField.Init(FilterComponent.Mode.Contains);
            ComponentFilterDropdown();

        }


        public ISceneFilter GetFilter()
        {
            var mode = (FilterComponent.Mode)_modeField.value;
            var filterValue = _dropField.text;
            return new FilterComponent(filterValue, mode);
        }

        public bool ValidateGameObject(GameObject go)
        {
            return new FilterComponent(_dropField.text, (FilterComponent.Mode)_modeField.value).MeetsRequirements(go);
        }

        public void Destroy()
        {
            DestroyWithoutNotification();
            OnDelete?.Invoke(this);
        }

        public void DestroyWithoutNotification() => _root.Remove(_el);

      
        private void ComponentFilterDropdown()
        {
            
            var componentNames = new HashSet<string>();

            
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
             var scene = SceneManager.GetSceneAt(i);
             var rootObjects = scene.GetRootGameObjects();
            
                foreach (var rootObject in rootObjects)
                {
                    
                    var components = rootObject.GetComponents(typeof(Component));

                  
                    foreach (var component in components)
                    {
                       
                        var componentName = component.GetType().Name;

                       
                        componentNames.Add(componentName);
                    }
                }
            }

            var choices = new List<string>(componentNames);

            _dropField.choices = choices;

         
            if (choices.Count > 0)
            {
                _dropField.value = choices[0];
            }

        }
    }
}