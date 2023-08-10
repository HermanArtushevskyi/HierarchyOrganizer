using System;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.Conditions
{
	public abstract class ConditionBase : ScriptableObject, ICondition
	{
		protected Predicate<GameObject> Condition;

		public bool IsMet(GameObject subject) => Condition.Invoke(subject);
	}
}