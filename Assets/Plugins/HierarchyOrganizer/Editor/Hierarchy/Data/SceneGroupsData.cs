using System;
using System.Collections.Generic;
using System.Linq;
using HierarchyOrganizer.Editor.Common.SerializedTuple;
using HierarchyOrganizer.Editor.Hierarchy.ScriptableObjectAdapters.Groups;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using UnityEditor;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.Data
{
	public class SceneGroupsData : ScriptableObject
	{
		private const string PATH = "Assets/Plugins/HierarchyOrganizer/Editor/Hierarchy/SceneData.asset";
		
		[SerializeField] public SceneGroupsGUIDTuple[] SceneGroups;
		
		public static void SetSceneGroups(string sceneName,string[] guids)
		{
			SceneGroupsData loadedData = AssetDatabase.LoadAssetAtPath<SceneGroupsData>(PATH);
			
			if (loadedData.SceneGroups == null) loadedData.SceneGroups = Array.Empty<SceneGroupsGUIDTuple>();

			List<SceneGroupsGUIDTuple> sceneGroupsList = loadedData.SceneGroups.ToList();

			bool found = false;
			
			foreach (SceneGroupsGUIDTuple tuple in sceneGroupsList)
			{
				if (tuple.Item1 == sceneName)
				{
					found = true;
					tuple.Item2 = guids;
				}
			}
			
			if (!found) sceneGroupsList.Add(new SceneGroupsGUIDTuple(sceneName, guids));

			loadedData.SceneGroups = sceneGroupsList.ToArray();
			
			EditorUtility.SetDirty(loadedData);
			AssetDatabase.SaveAssetIfDirty(loadedData);
			AssetDatabase.Refresh();
		}
		
		public static IGroup[] GetSceneGroups(string sceneName)
		{
			SceneGroupsGUIDTuple[] allSceneGroups =
				AssetDatabase.LoadAssetAtPath<SceneGroupsData>(PATH).SceneGroups;

			if (allSceneGroups == null) return new IGroup[]{};

			string[] allGroupObjects = AssetDatabase.FindAssets("t:GroupScriptableObject");

			string[] sceneGroupsGuid = {};

			foreach (SceneGroupsGUIDTuple tuple in allSceneGroups)
			{
				if (tuple.Item1 == sceneName) sceneGroupsGuid = tuple.Item2;
			}

			List<IGroup> res = new List<IGroup>();

			foreach (string guid in allGroupObjects)
			{
				if (sceneGroupsGuid.Contains(guid))
				{
					res.Add(AssetDatabase.LoadAssetAtPath<GroupScriptableObject>(
						AssetDatabase.GUIDToAssetPath(guid)));
				}
			}

			return res.ToArray();
		}
	}
}