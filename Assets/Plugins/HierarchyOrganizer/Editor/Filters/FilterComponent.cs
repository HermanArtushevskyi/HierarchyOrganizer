using System;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Filters
{
    public sealed class FilterComponent : FilterBase
    {
        

        private string _text;

        public FilterComponent(string text = null, Mode mode = Mode.Contains)
        {
            _text = text;

            switch (mode)
            {
                case Mode.Contains:
                    Filter = IncludePredicate;
                    break;
                case Mode.Exclude:
                    Filter = ExcludePredicate;
                    break;
            }
        }

        public enum Mode
        {
            Contains,
            Exclude
        }

        private bool IncludePredicate(GameObject go)
        {
            Component[] components = go.GetComponents<Component>();

            foreach (Component component in components)
            {
                if (component.GetType().Name == _text)
                    return true;
            }

            return false;
        }

        private bool ExcludePredicate(GameObject go)
        {
            return !IncludePredicate(go);
        }

    }
}
