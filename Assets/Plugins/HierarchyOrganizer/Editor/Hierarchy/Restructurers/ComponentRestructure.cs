using System;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.Restructurers
{
	public class ComponentRestructure : RestructureBase
	{
		private Type _componentType;

		public ComponentRestructure(Type componentType)
		{
			_componentType = componentType;
		}

		public override void Do(GameObject obj)
		{
			obj.AddComponent(_componentType);
		}

		public override void Undo(GameObject obj)
		{
			GameObject.Destroy(obj.GetComponent(_componentType));
		}
	}
}