using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.Conditions
{
    public class LayerCondition : ConditionBase
    {
        private int _value;

        public enum Mode
        {
            Is,
            Except
        }

        public LayerCondition(Mode mode, int value)
        {
            _value = value;

            if (mode == Mode.Is) Condition = IsPredicate;
            else if (mode == Mode.Except) Condition = ExceptPredicate;
        }

        private bool IsPredicate(GameObject go) => go.layer == _value;
        private bool ExceptPredicate(GameObject go) => go.layer != _value;
    }
}