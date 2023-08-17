using System;
using HierarchyOrganizer.Editor.Hierarchy.ScriptableObjectAdapters.Groups;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Windows;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Hierarchy.Windows.Common.GroupVariable
{
	public class GroupVariableAdapter : IViewAdapter
	{
		private const string UXML_PATH =
			"Assets/Plugins/HierarchyOrganizer/Editor/Hierarchy/Windows/Common/GroupVariable/UXML/GroupVariableView.uxml";

		private VisualElement _root;
		private TemplateContainer _el;
		private ObjectField _field;

		public event Action OnDestroy;
		public event Action<GroupVariableAdapter> OnDestroyThis;

		public void Init(VisualElement root)
		{
			_root = root;
			_el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH).Instantiate();
			_field = _el.Q<ObjectField>();
			
			_el.Q<Button>().clicked += Destroy;
			
			_field.objectType = typeof(GroupScriptableObject);
			
			root.Add(_el);
		}

		public void Init(VisualElement root, GroupScriptableObject group)
		{
			Init(root);

			_field.value = group;
		}

		public void Destroy()
		{
			DestroyWithoutNotification();
			OnDestroy?.Invoke();
			OnDestroyThis?.Invoke(this);
		}

		public object GetData()
		{
			return _field.value;
		}

		public void DestroyWithoutNotification()
		{
			_root.Remove(_el);
		}
	}
}