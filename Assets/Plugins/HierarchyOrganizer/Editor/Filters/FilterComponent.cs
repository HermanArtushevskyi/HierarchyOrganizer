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

        private bool PredicateFunc(GameObject go)
        {
            Component[] components = go.GetComponents<Component>();
            foreach (Component component in components)
            {
                if (_componentFilter.Invoke(component.GetType().Name))
                {
                    return true;
                }
            }
            return false;
        }


        private bool IsPredicate(Component component) => _componentFilter.Invoke(component.GetType().Name);

        private bool ContainsPredicate(Component component) => component.GetType().Name.Contains(_value);


        private bool ExcludePredicate(Component component) => !_componentFilter.Invoke(component.GetType().Name);

    }
}