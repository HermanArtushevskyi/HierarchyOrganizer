using System;
using HierarchyOrganizer.Editor.Common;
using HierarchyOrganizer.Editor.Hierarchy.Restructurers;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Factories;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.ScriptableObjectAdapters.Restructures
{
	[CreateAssetMenu(fileName = "Component", menuName = "HierarchyOrganizer/Restructures/ComponentRestructure")]
	public class ComponentRestructureAdapter : ScriptableObject, IRestructureFactory
	{
		[SerializeField] private MonoBehaviourReference _reference;
		[SerializeField] private ComponentRestructure.Mode _mode;

		public IRestructure Create()
		{ 
			return	new ComponentRestructure(HierarchyProjectUtils.GetMonoBehaviourByName(
											_reference.MonoBehaviourName), _mode);
		}
	}
}