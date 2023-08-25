using System.Collections.Generic;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Common.Extensions
{
    internal static class GameObjectExtensions
    {
        public static GameObject[] GetAllKids(this GameObject obj)
        {
            List<GameObject> res = new();
            
            foreach (Transform childTransform in obj.GetComponentsInChildren<Transform>())
            {
                if (childTransform.gameObject.name != obj.name) res.Add(childTransform.gameObject);
            }

            return res.ToArray();
        }

        public static GameObject GetChildByName(this GameObject obj, string name)
        {
            GameObject child = null;

            GameObject[] allKids = GetAllKids(obj);

            foreach (GameObject kid in allKids)
                if (kid.name == name)
                    child = kid;

            return child;
        }

        public static bool IsObjectInHierarchy(this GameObject obj, HierarchyTree tree)
        {
            if (tree.NodesStructure.Length == 0) return true;

            GameObject currentRoot = obj;
            
            for (int i = tree.NodesStructure.Length - 1; i >= 0; i--)
            {
                if (currentRoot.transform.parent == null) return false;
                if (currentRoot.transform.parent.name != tree.NodesStructure[i].Name) return false;

                currentRoot = currentRoot.transform.parent.gameObject;
            }

            return true;
        }

        public static bool HasKidWithName(GameObject obj, string name, out GameObject kid)
        {
            kid = null;
            
            GameObject[] allKids = GetAllKids(obj);

            foreach (GameObject child in allKids)
            {
                if (child.name == name)
                {
                    kid = child;
                    return true;
                }
            }

            return false;
        }
    }
}