using System;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.Conditions
{
	public class TagCondition : ConditionBase
	{
		private readonly string _value;

		private Predicate<string> _tagPredicate;

		public enum Mode
		{
			Is,
			Contains,
			Except
		}

		public TagCondition(Mode mode, string value)
		{
			_value = value;

			if (mode == Mode.Is) _tagPredicate = IsPredicate;
			else if (mode == Mode.Contains) _tagPredicate = ContainsPredicate;
			else if (mode == Mode.Except) _tagPredicate = ExcludePredicate;
			
			Condition = (go) => _tagPredicate.Invoke(go.tag);
		}

		
		private bool IsPredicate(string tag) => tag == _value;
		private bool ContainsPredicate(string tag) => tag.Contains(_value);
		private bool ExcludePredicate(string tag) => tag != _value;
	}
}