using System;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Interfaces.Filters
{
	public interface ISceneFilterElementAdapter : IVisualElementAdapter
	{
		public event Action<ISceneFilterElementAdapter> OnDelete;
		
		public ISceneFilter GetFilter();
		public bool ValidateGameObject(GameObject go);
	}
}