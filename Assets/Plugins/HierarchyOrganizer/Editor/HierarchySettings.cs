using System;

namespace HierarchyOrganizer.Editor
{
	public static class HierarchySettings
	{
		public static bool SpecificSettingsForEachScene = false;
		public static bool NestedHierarchy = true;

		public static Action SettingsChanged;
	}
}