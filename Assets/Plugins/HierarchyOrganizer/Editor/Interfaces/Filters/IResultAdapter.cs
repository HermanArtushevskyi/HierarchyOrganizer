using UnityEngine;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Interfaces.Filters
{
	public interface IResultElementAdapter : IVisualElementAdapter
	{
		public void Init(VisualElement root, GameObject data);
	}
}