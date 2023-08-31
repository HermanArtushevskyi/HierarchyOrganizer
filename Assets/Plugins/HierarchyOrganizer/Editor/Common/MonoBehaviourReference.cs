using System;

namespace HierarchyOrganizer.Editor.Common
{
    [Serializable]
	public class MonoBehaviourReference
	{
		public string MonoBehaviourName;

		public MonoBehaviourReference(string monoBehaviourName)
		{
			MonoBehaviourName = monoBehaviourName;
		}

		public override string ToString()
		{
			return MonoBehaviourName;
		}
	}
}