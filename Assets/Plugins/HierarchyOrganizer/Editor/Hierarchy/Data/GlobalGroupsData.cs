using System;
using System.Collections.Generic;
using System.Linq;
using HierarchyOrganizer.Editor.Hierarchy.ScriptableObjectAdapters.Groups;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using UnityEditor;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.Data
{
	[Serializable]
	public class GlobalGroupsData : ScriptableObject
	{
		public const string PATH = "Assets/Plugins/HierarchyOrganizer/Editor/Hierarchy/GlobalData.asset";

		[SerializeField] public string[] GlobalGroupsGUID;

		public static void SetGlobalGroups(string[] groupsGUID)
		{
			GlobalGroupsData data = AssetDatabase.LoadAssetAtPath<GlobalGroupsData>(PATH);

			if (data.GlobalGroupsGUID == null) data.GlobalGroupsGUID = Array.Empty<string>();

			data.GlobalGroupsGUID = groupsGUID;
			
			EditorUtility.SetDirty(data);
			AssetDatabase.SaveAssetIfDirty(data);
			AssetDatabase.Refresh();
		}

		public static IGroup[] GetAllGlobalGroups()
		{
			string[] globalGroups =
				AssetDatabase.LoadAssetAtPath<GlobalGroupsData>(PATH).GlobalGroupsGUID;

			string[] allGroupObjects = AssetDatabase.FindAssets("t:GroupScriptableObject");

			List<IGroup> res = new List<IGroup>();

			foreach (string guid in allGroupObjects)
			{
				if (globalGroups.Contains(guid))
				{
					res.Add(AssetDatabase.LoadAssetAtPath<GroupScriptableObject>(
						AssetDatabase.GUIDToAssetPath(guid)));
				}
			}

			return res.ToArray();
		}

		public static bool ObjectMeetsRequirements(GameObject obj, IGroup group)
		{
			bool meets = true;
			
			foreach (ICondition condition in group.Conditions)
				if (!condition.IsMet(obj))
					meets = false;

			return meets;
		}
	}
}