using System;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Interfaces.Hierarchy.Windows
{
	public interface IViewPresenter
	{
		public event Action OnDestroy;
		public void Init(VisualElement root);
		public void Destroy();
	}
}