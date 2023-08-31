using System;

namespace HierarchyOrganizer.Editor.Hierarchy.Conditions
{
	public class NameCondition : ConditionBase
	{
		private readonly Mode _mode;
		private readonly string _value;

		private Predicate<string> _tagPredicate;

		public enum Mode
		{
			Is,
			Contains,
			Exclude
		}

		public NameCondition(Mode mode, string value)
		{
			_mode = mode;
			_value = value;

			if (mode == Mode.Is) _tagPredicate = IsPredicate;
			else if (mode == Mode.Contains) _tagPredicate = ContainsPredicate;
			else if (mode == Mode.Exclude) _tagPredicate = ExcludePredicate;
			
			Condition = (go) => _tagPredicate.Invoke(go.name);
		}

		private bool IsPredicate(string name) => name == _value;
		private bool ContainsPredicate(string name) => name.Contains(_value);
		private bool ExcludePredicate(string name) => name != _value;
	}
}