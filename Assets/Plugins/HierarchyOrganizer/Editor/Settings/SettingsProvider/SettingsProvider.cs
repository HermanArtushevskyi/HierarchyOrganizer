using UnityEditor;

namespace HierarchyOrganizer.Editor.Settings
{
	public static class SettingsProvider
	{
		public static string GetPluginPath()
		{
			if (EditorPrefs.HasKey(nameof(HierarchySettings.PluginPath)))
				return EditorPrefs.GetString(nameof(HierarchySettings.PluginPath));

			return HierarchySettings.PluginPath;
		}
	}
}