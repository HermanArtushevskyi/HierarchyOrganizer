using System;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Interfaces.Filters
{
	public interface IVisualElementAdapter
	{
		public void Init(VisualElement root);
		public void Destroy();
		public void DestroyWithoutNotification();
	}
}