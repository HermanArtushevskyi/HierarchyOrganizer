using System.Collections.Generic;
using System.IO;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HierarchyOrganizer.Editor.Hierarchy.Data
{
	public class GlobalGroupsData
	{
		public List<IGroup> GlobalGroups { get; private set; }
		
		private static GlobalGroupsData _instance = null;

		private const string FILE_PATH = "/Plugins/HierarchyOrganizer/Editor/Hierarchy/Data/globalgroups.data";

		public static GlobalGroupsData GetInstance()
		{
			if (_instance != null) return _instance;

			return new GlobalGroupsData();
		}

		private GlobalGroupsData()
		{
			if (!File.Exists(Application.dataPath + FILE_PATH))
			{
				GlobalGroupsData instance = this;
				GlobalGroups = new List<IGroup>();
				_instance = instance;
			}
			else _instance = LoadData(Application.dataPath + FILE_PATH);
		}

		private GlobalGroupsData LoadData(string path)
		{
			string jsonData = File.ReadAllText(path);
			GlobalGroupsData groupsData = JsonConvert.DeserializeObject<GlobalGroupsData>(jsonData);
			return groupsData;
		}

		public void SaveData()
		{
			string jsonData = JsonConvert.SerializeObject(this);
			File.WriteAllText(Application.dataPath + FILE_PATH, jsonData);
		}
		
	}
}