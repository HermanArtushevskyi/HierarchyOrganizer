using HierarchyOrganizer.Editor.Hierarchy.Restructurers;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Factories;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.ScriptableObjectAdapters.Restructures
{
	public class StaticStatusRestructureAdapter : ScriptableObject, IRestructureFactory
	{
		[SerializeField] private bool Value;
		[SerializeField] private StaticStatusRestructure.Mode Mode;
		
		public IRestructure Create() => new StaticStatusRestructure(Value, Mode);
	}
}