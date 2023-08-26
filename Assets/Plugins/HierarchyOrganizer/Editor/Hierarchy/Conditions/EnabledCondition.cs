using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.Conditions
{
    public class EnabledCondition : ConditionBase
    {
        private bool _value;
        
        public EnabledCondition(bool value, Mode mode)
        {
            _value = value;

            if (mode == Mode.Is) Condition = IsPredicate;
        }

        public enum Mode
        {
            Is
        }

        private bool IsPredicate(GameObject go) => go.activeInHierarchy == _value;
    }
}