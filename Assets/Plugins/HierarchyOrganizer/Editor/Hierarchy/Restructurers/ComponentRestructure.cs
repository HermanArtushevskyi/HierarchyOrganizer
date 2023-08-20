using System;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.Restructurers
{
	public class ComponentRestructure : RestructureBase
	{
		private Type _componentType;

		private Action<GameObject> _doAction;
		private Action<GameObject> _undoAction;

		public ComponentRestructure(Type componentType, Mode mode)
		{
			_componentType = componentType;

			switch (mode)
			{
				case Mode.Add:
					_doAction = Add;
					_undoAction = Remove;
					break;
				case Mode.Remove:
					_doAction = Remove;
					_undoAction = Add;
					break;
			}
		}

		public enum Mode
		{
			Add,
			Remove
		}

		public override void Do(GameObject obj) => _doAction?.Invoke(obj);

		public override void Undo(GameObject obj) => _undoAction?.Invoke(obj);

		private void Add(GameObject go)
		{
			go.AddComponent(_componentType);
		}

		private void Remove(GameObject go)
		{
			GameObject.Destroy(go.GetComponent(_componentType));
		}
	}
}