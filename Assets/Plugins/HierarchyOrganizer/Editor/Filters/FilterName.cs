using System;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Filters
{
    public sealed class FilterName : FilterBase
    {
        private readonly Predicate<string> _nameFilter;

        private string _value;

        public FilterName(string value = null, Mode mode = Mode.Is)
        {
            _value = value;

            switch (mode)
            {
                case Mode.Is:
                    _nameFilter = IsPredicate;
                    break;
                case Mode.Contains:
                    _nameFilter = ContainsPredicate;
                    break;
                case Mode.Exclude:
                    _nameFilter = ExcludePredicate;
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

        private bool PredicateFunc(GameObject go) => _nameFilter.Invoke(go.name);

        private bool IsPredicate(string name) => name == _value;

        private bool ContainsPredicate(string name) => name.Contains(_value);

        private bool ExcludePredicate(string name) => name != _value;
    }
}