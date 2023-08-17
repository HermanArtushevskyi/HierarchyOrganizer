using System;
using System.Collections.Generic;
using System.Reflection;
using HierarchyOrganizer.Editor.Interfaces.EditorView;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Settings
{
	public class SettingsEditor : EditorWindow
	{
		private const string ROOT_XML_PATH =
			"Assets/Plugins/HierarchyOrganizer/Editor/Settings/UXML/SettingsViewDocument.uxml";
		
		private static SettingsEditor window { get; set; }
		
		private List<ISettingsVariable> _variables;
		private ScrollView _scrollView;
		
		[MenuItem("LonelyStudio/HierarchyOrganizer/Settings", priority = 0)]
		private static void ShowWindow()
		{
			window = GetWindow<SettingsEditor>();
			window.titleContent = new GUIContent("Hierarchy Settings");
			window.Show();
		}
		
		private void CreateGUI()
		{
			rootVisualElement.Add(AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(ROOT_XML_PATH).Instantiate());
		
			_scrollView = rootVisualElement.Q<ScrollView>();
			_variables = AnalyzeSettings();

			Button saveBtn = rootVisualElement.Q<Button>();
			saveBtn.clicked += SaveClicked;
		}

		private void SaveClicked()
		{
			if (!EditorUtility.DisplayDialog("Saving", "Save values?", "Yes", "No")) return;
			
			foreach (ISettingsVariable variable in _variables)
			{
				variable.SaveValue();
			}
			
			this.Close();
		}

		private List<ISettingsVariable> AnalyzeSettings()
		{
			List<ISettingsVariable> variables = new List<ISettingsVariable>();
			
			Type settingsType = typeof(HierarchySettings);
			
			foreach (FieldInfo field in settingsType.GetFields())
			{
				if (ProcessVariable(field, out ISettingsVariable variable))
					variables.Add(variable);
			}
			
			return variables;
		}

		private bool ProcessVariable(FieldInfo field, out ISettingsVariable variable)
		{
			string fieldName = field.Name;
			string fieldAlias = null;

			VariableAliasAttribute aliasAttribute = field.GetCustomAttribute<VariableAliasAttribute>();

			if (aliasAttribute != null) fieldAlias = aliasAttribute.Alias;
			
			if (field.FieldType == typeof(bool))
			{
				variable = new SettingsVariableBool(fieldName, fieldAlias, _scrollView);
				return true;
			}

			Debug.LogWarning($"HierarchySettings: Type {field.FieldType} is not supported");

			variable = null;
			return false;
		}
	}
}