using System;

namespace HierarchyOrganizer.Editor.Settings
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