using System;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Filters
{
    public sealed class FilterComponent : FilterBase
    {
<<<<<<< HEAD
        private readonly Predicate<string> _componentFilter;

        private string _value;

        public FilterComponent(string value = null, Mode mode = Mode.Is)
        {
            _value = value;
=======
        

        private string _text;

        public FilterComponent(string text = null, Mode mode = Mode.Contains)
        {
            _text = text;
>>>>>>> 8e98faf4c32fc8385df620cce116a1f0141358e8

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
<<<<<<< HEAD
                if (component.GetType().Name == _value)
=======
                if (component.GetType().Name == _text)
>>>>>>> 8e98faf4c32fc8385df620cce116a1f0141358e8
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
