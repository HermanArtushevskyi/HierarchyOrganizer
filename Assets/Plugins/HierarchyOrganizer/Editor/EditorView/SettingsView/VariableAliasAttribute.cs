using System;

namespace HierarchyOrganizer.Editor.EditorView.SettingsView
{
	public class VariableAliasAttribute : Attribute
	{
		public readonly string Alias;

		public VariableAliasAttribute(string alias)
		{
			Alias = alias;
		}
	}
}