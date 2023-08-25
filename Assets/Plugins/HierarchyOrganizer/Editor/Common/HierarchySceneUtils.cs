using System.Collections.Generic;
using System.Linq;
using HierarchyOrganizer.Editor.Common.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HierarchyOrganizer.Editor.Common
{
	public static class HierarchySceneUtils
	{
		public static GameObject[] GetAllObjectsOnScene(Scene scene)
		{
			List<GameObject> roots = new List<GameObject>(scene.rootCount);

			scene.GetRootGameObjects(roots);

			List<GameObject> allObjects = new();

			foreach (GameObject root in roots)
			{
				allObjects.Add(root);
				Transform[] children = root.GetComponentsInChildren<Transform>();
				foreach (Transform child in children)
				{
					if (!roots.Contains(child.gameObject))
						allObjects.Add(child.gameObject);
				}
			}
			
			return allObjects.ToArray();
		}

		public static GameObject GetObjectOnSceneByName(Scene scene, string name)
		{
			GameObject[] allObjects = GetAllObjectsOnScene(scene);

			foreach (GameObject go in allObjects)
			{
				if (go.name == name) return go;
			}

			return null;
		}

		public static GameObject GetRootByName(Scene scene, string name)
		{
			List<GameObject> roots = new List<GameObject>(scene.rootCount);

			scene.GetRootGameObjects(roots);

			foreach (GameObject root in roots)
			{
				if (root.name == name) return root;
			}

			return null;
		}

		public static GameObject[] GetAllObjectsInHierarchy(Scene scene, HierarchyTree tree)
		{
			if (tree.IsRelative)
			{
				GameObject root = GetObjectOnSceneByName(scene, tree.NodesStructure[0].Name);
				return CommonSearchAlgo(root, tree);
			}
			else
			{
				GameObject root = GetRootByName(scene, tree.NodesStructure[0].Name);
				return CommonSearchAlgo(root, tree);
			}
		}
        
		private static GameObject[] CommonSearchAlgo(GameObject root, HierarchyTree tree)
		{
			GameObject currentRoot = root;
			
			for (ushort i = 1; i < tree.NodesStructure.Length; i++)
			{
				string currentSubjectName = tree.NodesStructure[i].Name;
				bool lastObject = i == tree.NodesStructure.Length - 1;

				if (lastObject)
				{
					return currentRoot.GetAllKids();
				}
				
				GameObject kidWithName = currentRoot.GetChildByName(currentSubjectName);

				if (kidWithName == null) return null;
				
				currentRoot = kidWithName;
			}

			return null;
		}
	}
}