using System;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Filters
{
	public sealed class FilterTag : FilterBase
	{
		private readonly Predicate<string> _tagFilter;
		
		public FilterTag(Predicate<string> filter) : base()
		{
			_tagFilter = filter;
			Filter = FilterFunc;
		}
		
		private bool FilterFunc(GameObject go) => _tagFilter.Invoke(go.name);
	}
}