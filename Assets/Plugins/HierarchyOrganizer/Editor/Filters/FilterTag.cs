using System;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Filters
{
	public sealed class FilterTag : FilterBase
	{
		public readonly Predicate<string> TagFilter;
		
		private string _value;

		public FilterTag(string value, Mode mode)
		{
			_value = value;
			
			switch (mode)
			{
				case Mode.Is:
					TagFilter = IsPredicate;
					break;
				case Mode.Contains:
					TagFilter = ContainsPredicate;
					break;
				case Mode.Exclude:
					TagFilter = ExcludePredicate;
					break;
			}

			Filter = PredicateFunc;
		}

		public enum Mode
		{
			Is,
			Contains,
			Exclude
		}

		private bool PredicateFunc(GameObject go) => TagFilter.Invoke(go.tag);

		private bool IsPredicate(string tag) => tag == _value;

		private bool ContainsPredicate(string tag) => tag.Contains(_value);

		private bool ExcludePredicate(string tag) => !tag.Contains(_value);
	}
}