using System;
using HierarchyOrganizer.Editor.Interfaces.Filters;
using UnityEngine;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Filters.UXMLAdapters
{
    public abstract class FilterAdapterBase : ISceneFilterElementAdapter
    {
        public event Action<ISceneFilterElementAdapter> OnDelete;

        protected VisualElement Root;
        protected TemplateContainer Element;

        protected bool IsInitiated;
        
        public abstract void Init(VisualElement root);

        public abstract ISceneFilter GetFilter();

        public abstract bool ValidateGameObject(GameObject go);

        public void Destroy()
        {
            DestroyWithoutNotification();
            OnDelete?.Invoke(this);
        }

        public void DestroyWithoutNotification() => Root.Remove(Element);
    }
}