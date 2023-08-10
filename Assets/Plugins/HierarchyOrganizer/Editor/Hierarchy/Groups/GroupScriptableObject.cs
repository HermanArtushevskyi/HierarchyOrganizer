using System.Collections.Generic;
using System.Linq;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using TNRD;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.Groups
{
	[CreateAssetMenu(fileName = "Group", menuName = "HierarchyOrganizer/Group", order = 0)]
	public class GroupScriptableObject : ScriptableObject, IGroup
	{
		[SerializeField] private string _name;

		[SerializeField] private SerializableInterface<ICondition>[] _serializableConditions;
		[SerializeField] private SerializableInterface<IRestructure>[] _serializableRestructures;

		private ICondition[] _conditions
		{
			get
			{
				ICondition[] arr = new ICondition[_serializableConditions.Length];
				for (int i = 0; i < _serializableConditions.Length; i++)
				{
					arr[i] = _serializableConditions[i].Value;
				}

				return arr;
			}
		}

		private IRestructure[] _restructures
		{
			get
			{
				IRestructure[] arr = new IRestructure[_serializableRestructures.Length];
				for (int i = 0; i < _serializableRestructures.Length; i++)
				{
					arr[i] = _serializableRestructures[i].Value;
				}

				return arr;
			}
		}
		
		public string Name => _name;

		public List<ICondition> Conditions => _conditions.ToList();
		public List<IRestructure> Restructures => _restructures.ToList();
	}
}