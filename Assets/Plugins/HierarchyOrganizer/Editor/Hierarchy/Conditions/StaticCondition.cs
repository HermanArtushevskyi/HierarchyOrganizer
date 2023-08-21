using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.Conditions
{
    public class StaticCondition : ConditionBase
    {
        private readonly Mode _mode;
        private readonly bool _value;

        public StaticCondition(Mode mode, bool value)
        {
            _mode = mode;
            _value = value;

            if (mode == Mode.Is) Condition = IsPredicate;
        }

        public enum Mode
        {
            Is
        }

        private bool IsPredicate(GameObject go) => go.isStatic == _value;
    }
}