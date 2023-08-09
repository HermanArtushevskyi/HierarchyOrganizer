using System.Collections.Generic;

namespace HierarchyOrganizer.Editor.Interfaces.Hierarchy
{
	public interface IGroup
	{
		public string Name { get; }
		public List<ICondition> Conditions { get; }
		public List<IRestructure> Restructures { get; }
	}
}