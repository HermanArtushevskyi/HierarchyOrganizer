using System;
using Plugins.HierarchyOrganizer.Editor.Interfaces.Filters;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Filters
{
	public abstract class FilterBase : ISceneFilter
	{
		protected Predicate<GameObject> Filter;
		
		public bool MeetsRequirements(GameObject go)
		{
			return Filter.Invoke(go);
		}
	}
}