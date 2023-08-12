using System;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Filters
{
    public sealed class FilterComponent : FilterBase
    {
        private readonly Predicate<string> _componentFilter;

        private string _value;

        public FilterComponent(string value = null, Mode mode = Mode.Is)
        {
            _value = value;

            switch (mode)
            {
                case Mode.Contains:
                    _componentFilter = ContainsPredicate;
                    break;
                case Mode.Exclude:
                    _componentFilter = ExcludePredicate;
                    break;
            }

            Filter = PredicateFunc;
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
                if (component.GetType().Name == _value)
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
