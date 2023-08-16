using System;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using UnityEditor;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Common
{
	[Serializable]
	public abstract class SerializedTuple<T1, T2> : Tuple<T1, T2>
	{
		[SerializeField] private T1 value1;

		[SerializeField] private T2 value2;
		
		public SerializedTuple(T1 item1, T2 item2) : base(item1, item2)
		{
			value1 = item1;
			value2 = item2;
		}
		
		public new T1 Item1 => value1;
		public new T2 Item2 => value2;
	}

	public class SceneGroupsGUIDTuple : SerializedTuple<SceneAsset, string>
	{
		public SceneGroupsGUIDTuple(SceneAsset item1, string item2) : base(item1, item2)
		{
		}
	}
}