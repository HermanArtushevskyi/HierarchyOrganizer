using System;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.Restructurers
{
    public class LayerRestructure : RestructureBase
    {
        private int _oldValue;

        private readonly Action<GameObject> _doAction;
        private readonly Action<GameObject> _undoAction;
        
        public LayerRestructure(int value, Mode mode)
        {
            if (mode == Mode.Set)
            {
                _doAction = go =>
                {
                    _oldValue = go.layer;
                    go.layer = value;
                };

                _undoAction = go =>
                {
                    (go.layer, _oldValue) = (_oldValue, go.layer);
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