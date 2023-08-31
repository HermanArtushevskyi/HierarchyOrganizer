using System.Collections.Generic;
using System.Linq;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Factories;
using TNRD;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.ScriptableObjectAdapters.Groups
{
	[CreateAssetMenu(fileName = "Group", menuName = "HierarchyOrganizer/Group", order = -1)]
	public class GroupScriptableObject : ScriptableObject, IGroup
	{
		[SerializeField] private string _name;
		[SerializeField] [TextArea] private string _message;

		[SerializeField] private SerializableInterface<IConditionFactory>[] _serializableConditions;
		[SerializeField] private SerializableInterface<IRestructureFactory>[] _serializableRestructures;

		private ICondition[] _conditions
		{
			get
			{
				ICondition[] arr = new ICondition[_serializableConditions.Length];
				for (int i = 0; i < _serializableConditions.Length; i++)
				{
					arr[i] = _serializableConditions[i].Value.Create();
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
					arr[i] = _serializableRestructures[i].Value.Create();
				}

				return arr;
			}
		}
		
		public string Name => _name;
		public string Message => _message;

		public List<ICondition> Conditions => _conditions.ToList();
		public List<IRestructure> Restructures => _restructures.ToList();
	}
}