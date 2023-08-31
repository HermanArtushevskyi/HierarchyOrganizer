using System;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.Restructurers
{
    public class TagRestructure : RestructureBase
    {
        private Action<GameObject> _doAction;
        private Action<GameObject> _undoAction;

        private string _oldValue;
        
        public TagRestructure(string value, Mode mode)
        {
            if (mode == Mode.Set)
            {
                _doAction = go =>
                {
                    _oldValue = go.tag;
                    go.tag = value;
                };

                _undoAction = go =>
                {
                    (go.tag, _oldValue) = (_oldValue, go.tag);
                };
            }
        }

        public enum Mode
        {
            Set
        }
        
        public override void Do(GameObject obj) => _doAction.Invoke(obj);

        public override void Undo(GameObject obj) => _undoAction.Invoke(obj);
    }
}