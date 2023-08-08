using System;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.Conditions
{
	public abstract class ConditionBase : ICondition
	{
		protected Predicate<GameObject> Condition;

		public bool IsMet(GameObject subject) => Condition.Invoke(subject);

		public abstract void Do(GameObject obj);

		public abstract void Undo(GameObject obj);
	}
}