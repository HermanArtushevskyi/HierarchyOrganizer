using System.Reflection;
using HierarchyOrganizer.Editor.Interfaces.EditorView;
using UnityEditor;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Settings
{
	public abstract class SettingsVariableBase : ISettingsVariable
	{
		protected string VariableName;
		protected string VariableAlias;
		
		protected SettingsVariableBase(string name, string alias, ScrollView list)
		{
			VariableName = name;
			VariableAlias = alias;
			AddUxml(list);
		}

		public abstract void SetValue(object val);

		public abstract void SaveValue();

		protected abstract void AddUxml(ScrollView list);

		protected abstract object GetCurrentVariable();

		protected void ApplyAlias(Label label)
		{
			if (VariableAlias != null) label.text = VariableAlias;
		}
	}
}