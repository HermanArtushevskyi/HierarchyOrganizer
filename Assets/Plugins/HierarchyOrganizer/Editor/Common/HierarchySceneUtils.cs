using System.Collections.Generic;
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
	}
}