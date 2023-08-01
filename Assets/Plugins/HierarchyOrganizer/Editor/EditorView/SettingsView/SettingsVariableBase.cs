using System.Reflection;
using HierarchyOrganizer.Editor.Interfaces.EditorView;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.EditorView.SettingsView
{
	public abstract class SettingsVariableBase : ISettingsVariable
	{
		protected string VariableName;
		
		protected SettingsVariableBase(string name, ScrollView list)
		{
			VariableName = name;
			AddUxml(list);
		}

		public abstract void SetValue(object val);

		public abstract void SaveValue();

		protected abstract void AddUxml(ScrollView list);
		
		protected object GetCurrentVariable()
		{
			FieldInfo field = typeof(HierarchySettings).GetField(VariableName);
			return field.GetValue(null);
		}
	}
}