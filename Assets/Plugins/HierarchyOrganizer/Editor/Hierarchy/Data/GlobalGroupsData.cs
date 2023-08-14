using System.Collections.Generic;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HierarchyOrganizer.Editor.Hierarchy.Data
{
	public class GlobalGroupsData : ScriptableObject
	{
		public const string PATH = "Assets/Plugins/HierarchyOrganizer/Editor/Hierarchy/GlobalData.asset";

		[SerializeField] public IGroup[] GlobalGroups;
		[SerializeField] public Dictionary<Scene, IGroup[]> SceneGroups;

		public void SetGlobalGroups(IGroup[] groups)
		{
			GlobalGroups = groups;
		}
	}
}