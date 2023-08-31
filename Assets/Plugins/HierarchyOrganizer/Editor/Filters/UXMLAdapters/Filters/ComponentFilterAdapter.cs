using System.Linq;
using HierarchyOrganizer.Editor.Common;
using HierarchyOrganizer.Editor.Interfaces.Filters;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using SettingsProvider = HierarchyOrganizer.Editor.Settings.SettingsProvider;

namespace HierarchyOrganizer.Editor.Filters.UXMLAdapters
{
    public class ComponentFilterElementAdapter : FilterAdapterBase
    {
        private static readonly string _uxmlPath = SettingsProvider.GetPluginPath() + 
                                                   "Editor/Filters/UXML/Filters/ComponentFilterView.uxml";

        private EnumField _modeField;
        private DropdownField _dropField;

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

            _dropField = Element.Q<DropdownField>();
            _dropField.choices = HierarchyProjectUtils.GetAllMonoBehavioursNamesInProjectAsync().ToList();
            
            Element.Q<Button>().clicked += Destroy;

            _modeField.Init(FilterComponent.Mode.Contains);
            IsInitiated = true;
        }

        private void Reinitialize()
        {
            EnumField elEnum = Element.Q<EnumField>();
            elEnum.Init(_modeField.value);
            _modeField = elEnum;

            DropdownField elDropdown = Element.Q<DropdownField>();
            elDropdown.choices = HierarchyProjectUtils.GetAllMonoBehavioursNamesInProjectAsync().ToList();
            elDropdown.value = _dropField.value;
            _dropField = elDropdown;
            
            Element.Q<Button>().clicked += Destroy;
        }
        
        public override ISceneFilter GetFilter()
        {
            var mode = (FilterComponent.Mode)_modeField.value;
            var filterValue = _dropField.text;
            return new FilterComponent(filterValue, mode);
        }

        public override bool ValidateGameObject(GameObject go) => GetFilter().MeetsRequirements(go);
    }
}