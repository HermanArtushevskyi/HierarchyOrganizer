using System;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.Restructurers
{
	public class NameRestructure : IRestructure
	{
		private Action<GameObject> _doAction;
		private Action<GameObject> _undoAction;

		private string _oldName;
		
		public NameRestructure(string value, Mode mode)
		{
			switch (mode)
			{
				case Mode.Set:
					_doAction = (go) =>
					{
						_oldName = go.name;
						go.name = value;
					};
					break;
				case Mode.Add:
					_doAction = (go) =>
					{
						_oldName = go.name;
						go.name += value;
					};
					break;
			}

			_undoAction = (go) => go.name = _oldName;
		}

		public enum Mode
		{
			Set,
			Add
		}
		
		public void Do(GameObject obj) => _doAction?.Invoke(obj);

		public void Undo(GameObject obj) => _undoAction?.Invoke(obj);
	}
}