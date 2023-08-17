using System;

namespace HierarchyOrganizer.Editor.Common.SerializedTuple
{
	[Serializable]
	public class SceneGroupsGUIDTuple : SerializedTuple<string, string[]>
	{
		public SceneGroupsGUIDTuple(string item1, string[] item2) : base(item1, item2)
		{
		}
	}
}