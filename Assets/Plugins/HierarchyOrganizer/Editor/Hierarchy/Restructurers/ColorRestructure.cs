using UnityEditor;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Hierarchy.Restructurers
{
	public class ColorRestructure : RestructureBase
	{
		private readonly Color _color;

		public ColorRestructure(Color color)
		{
			_color = color;
		}

		public override void Do(GameObject obj)
		{
		}

		public override void Undo(GameObject obj)
		{
		}
	}
}