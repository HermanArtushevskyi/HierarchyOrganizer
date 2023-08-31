using System;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Interfaces.EditorView
{
	public interface ISettingsVariable
	{
		public void Init(VisualElement root);
		public void SetValue(object val);
		public void SaveValue();
	}
}