using System;
using HierarchyOrganizer.Editor.Settings;

namespace HierarchyOrganizer.Editor
{
	public static class HierarchySettings
	{
		[VariableAlias("Use specific settings for each scene")]
		public static bool SpecificSettingsForEachScene;
		[VariableAlias("Use nested type of hierarchy")]
		public static bool NestedHierarchy;
	}
}