﻿using System;
using System.Collections.Generic;
using System.Reflection;
using HierarchyOrganizer.Editor.Interfaces.EditorView;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.EditorView.SettingsView
{
	public class SettingsEditor : EditorWindow
	{
		private const string ROOT_XML_PATH =
			"Assets/Plugins/HierarchyOrganizer/Editor/EditorView/SettingsView/UXML/SettingsViewDocument.uxml";
		
		private static SettingsEditor window { get; set; }
		
		private List<ISettingsVariable> _variables;
		private ScrollView _scrollView;
		
		[MenuItem("LonelyStudio/HierarchyOrganizer/Settings")]
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
				if (field.FieldType == typeof(bool))
				{
					variables.Add(new SettingsVariableBool(field.Name, _scrollView));
				}
				else if (field.FieldType == typeof(Action)) continue;
				else
				{
					Debug.LogWarning($"HierarchySettings: Type {field.FieldType} is not supported");
				}
			}
			
			return variables;
		}
	}
}