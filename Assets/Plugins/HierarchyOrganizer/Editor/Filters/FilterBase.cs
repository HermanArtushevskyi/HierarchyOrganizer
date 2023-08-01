using System;
using Plugins.HierarchyOrganizer.Editor.Interfaces.Filters;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Filters
{
	public abstract class FilterBase : ISceneFilter
	{
		protected Predicate<GameObject> Filter;

		public GameObject[] GetObjects()
		{
			return null;
		}

		public GameObject GetObject()
		{
			return null;
		}
	}
}