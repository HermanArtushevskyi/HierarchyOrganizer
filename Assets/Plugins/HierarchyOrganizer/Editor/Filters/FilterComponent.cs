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
                case Mode.Is:
                    _componentFilter = IsPredicate;
                    break;
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
            Is,
            Contains,
            Exclude
        }

        private bool PredicateFunc(GameObject go) => _componentFilter.Invoke(go.tag);

        private bool IsPredicate(string componentName) => componentName == _value;

        private bool ContainsPredicate(string componentName) => componentName.Contains(_value);

        private bool ExcludePredicate(string componentName) => componentName != _value;
    }
}