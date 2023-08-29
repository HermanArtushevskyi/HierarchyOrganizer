using System;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Filters
{
	public sealed class FilterTag : FilterBase
	{
		private readonly Predicate<string> _tagFilter;
		
		private string _value;

		public FilterTag(string value = null, Mode mode = Mode.Is)
		{
			_value = value;
			
			switch (mode)
			{
				case Mode.Is:
					_tagFilter = IsPredicate;
					break;
				case Mode.Contains:
					_tagFilter = ContainsPredicate;
					break;
				case Mode.Exclude:
					_tagFilter = ExcludePredicate;
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

		private bool PredicateFunc(GameObject go) => _tagFilter.Invoke(go.tag);

		private bool IsPredicate(string tag) => tag == _value;

		private bool ContainsPredicate(string tag) => tag.Contains(_value);

		private bool ExcludePredicate(string tag) => tag != _value;
	}
}