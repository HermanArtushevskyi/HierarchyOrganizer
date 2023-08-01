using System;

namespace HierarchyOrganizer.Editor.Interfaces.EditorView
{
	public interface ISettingsVariable
	{
		public void SetValue(object val);
		public void SaveValue();
	}
}