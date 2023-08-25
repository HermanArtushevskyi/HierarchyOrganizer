using System;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.Restructurers
{
    public class EnabledRestructure : RestructureBase
    {
        private readonly bool _value;

        private bool _oldValue;
        
        private Action<GameObject> _doAction;
        private Action<GameObject> _undoAction;


        public EnabledRestructure(bool value, Mode mode)
        {
            _value = value;
            if (mode == Mode.Set)
            {
                _doAction = go =>
                {
                    _oldValue = go.activeInHierarchy;
                    go.SetActive(_value);
                };

                _undoAction = go =>
                {
                    bool currentValue = go.activeInHierarchy;
                    go.SetActive(_oldValue);
                    _oldValue = currentValue;
                };
            }
            
            else if (mode == Mode.Switch)
            {
                _doAction = go =>
                {
                    go.SetActive(!go.activeInHierarchy);
                };

                _undoAction = go =>
                {
                    go.SetActive(!go.activeInHierarchy);
                };
            }
        }

        public enum Mode
        {
            Set,
            Switch
        }
        
        public override void Do(GameObject obj) => _doAction.Invoke(obj);

        public override void Undo(GameObject obj) => _undoAction.Invoke(obj);
    }
}