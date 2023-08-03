using System;
using Plugins.HierarchyOrganizer.Editor.Interfaces.Filters;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Filters
{
	public abstract class FilterBase : ISceneFilter
	{
		public Predicate<GameObject> Filter { get; protected set; }

		protected FilterBase()
		{
		}

		protected FilterBase(Predicate<GameObject> filter)
		{
			Filter = filter;
		}

		public bool MeetsRequirements(GameObject go)
		{
			return Filter.Invoke(go);
		}
	}
}