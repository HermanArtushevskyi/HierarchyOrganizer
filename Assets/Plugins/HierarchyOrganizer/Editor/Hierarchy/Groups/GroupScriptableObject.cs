using System.Collections.Generic;
using System.Linq;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.Groups
{
	[CreateAssetMenu(fileName = "Group", menuName = "HierarchyOrganizer/Group", order = 0)]
	public class GroupScriptableObject : ScriptableObject, IGroup
	{
		[SerializeField] private string _name;
		[SerializeField] private ICondition[] _conditions;
		[SerializeField] private IRestructure[] _restructures;
		
		public string Name => _name;

		public List<ICondition> Conditions => _conditions.ToList();
		public List<IRestructure> Restructures => _restructures.ToList();
	}
}