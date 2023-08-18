using System;
using System.Reflection;
using HierarchyOrganizer.Editor.Interfaces.EditorView;
using UnityEngine;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Settings
{
	public abstract class SettingsVariableBase : ISettingsVariable
	{
		protected string VariableName;
		protected string VariableAlias;
		
		protected SettingsVariableBase(string name, string alias)
		{
			VariableName = name;
			VariableAlias = alias;
		}

		public abstract void SetValue(object val);

		public abstract void SaveValue();
		public void Init(VisualElement root)
		{
			AddUxml(root);
		}

		protected abstract void AddUxml(VisualElement list);

		protected abstract object GetCurrentVariable();

		protected void ApplyAlias(Label label)
		{
			if (VariableAlias != null) label.text = VariableAlias;
		}

		public static ISettingsVariable ProcessVariable(Type type, string name)
		{
			ISettingsVariable result;
			string alias = null;
			
			VariableAliasAttribute aliasAttribute = type.GetCustomAttribute<VariableAliasAttribute>();

			if (aliasAttribute != null) alias = aliasAttribute.Alias;
			
			if (type == typeof(bool))
				return new SettingsVariableBool(name, alias);

			if (type == typeof(string))
				return new SettingsVariableString(name, alias);

			Debug.LogWarning($"HierarchySettings: Type {type} is not supported");

			result = null;
			return result;
		}
	}
}