using System;
using System.Collections.Generic;
using System.Linq;
using HierarchyOrganizer.Editor.Hierarchy.Groups;
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
		
		public void SetGlobalGroups(string[] groupsGUID)
		{
			GlobalGroupsGUID = groupsGUID;
		}

		public static IGroup[] GetAllGlobalGroups()
		{
			string[] globalGroups = AssetDatabase.LoadAssetAtPath<GlobalGroupsData>(GlobalGroupsData.PATH).GlobalGroupsGUID;
			
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
	}
}