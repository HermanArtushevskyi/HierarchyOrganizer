using System;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.Data
{
	[CreateAssetMenu(fileName = "Global data", menuName = "HierarchyOrganizer/GlobalData")]
	[Serializable]
	public class GlobalGroupsData : ScriptableObject
	{
		public const string PATH = "Assets/Plugins/HierarchyOrganizer/Editor/Hierarchy/GlobalData.asset";
		
		[SerializeField] public string[] GlobalGroupsGUID;
		
		public void SetGlobalGroups(string[] groupsGUID)
		{
			GlobalGroupsGUID = groupsGUID;
		}
		
	}
}