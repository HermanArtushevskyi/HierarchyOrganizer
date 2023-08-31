using System;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.Restructurers
{
	public class StaticStatusRestructure : RestructureBase
	{
		private readonly bool _value;
		
		private readonly Action<GameObject> _doAction;
		private readonly Action<GameObject> _undoAction;

		public StaticStatusRestructure(bool value, Mode mode)
		{
			_value = value;

			switch (mode)
			{
				case Mode.Set:
					_doAction = SetAction;
					_undoAction = UndoSetAction;
					break;
				case Mode.Switch:
					_doAction = SwitchAction;
					_undoAction = SwitchAction;
					break;
			}
		}

		public enum Mode
		{
			Set,
			Switch
		}
		
		public override void Do(GameObject obj) => _doAction?.Invoke(obj);

		public override void Undo(GameObject obj) => _undoAction?.Invoke(obj);

		private void SetAction(GameObject go) => go.isStatic = _value;

		private void UndoSetAction(GameObject go) => go.isStatic = !_value;

		private void SwitchAction(GameObject go) => go.isStatic = !go.isStatic;
	}
}