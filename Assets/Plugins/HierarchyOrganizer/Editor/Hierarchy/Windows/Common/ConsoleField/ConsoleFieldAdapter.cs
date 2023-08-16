using System;
using System.Collections.Generic;
using HierarchyOrganizer.Editor.Hierarchy.Groups;
using HierarchyOrganizer.Editor.Hierarchy.Windows.ConsoleWindow;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Windows;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace HierarchyOrganizer.Editor.Hierarchy.Windows.Common.ConsoleField
{
	public class ConsoleFieldAdapter : IViewAdapter
	{
		private const string UXML_PATH =
			"Assets/Plugins/HierarchyOrganizer/Editor/Hierarchy/Windows/Common/ConsoleField/UXML/ConsoleFieldView.uxml";

		private VisualElement _root;
		private TemplateContainer _el;
		private ObjectField _goField;
		private ObjectField _groupField;
		private TextField _textArea;
		private Button _fixBtn;

		private IGroup _group;
		private GameObject _go;

		public event Action OnDestroy;
		public event Action<ConsoleFieldAdapter> OnDestroyThis;
	
		public void Init(VisualElement root)
		{
			_root = root;
			_el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH).Instantiate();
			_root.Add(_el);

			_goField = _el.Q<ObjectField>("goField");
			_groupField = _el.Q<ObjectField>("groupField");
			_textArea = _el.Q<TextField>();
			_fixBtn = _el.Q<Button>();
		}

		public void Init(VisualElement root, IGroup group, GameObject go)
		{
			Init(root);

			_group = group;
			_go = go;

			_goField.objectType = typeof(GameObject);
			_goField.value = go;
			_goField.SetEnabled(false);
			
			_groupField.objectType = typeof(GroupScriptableObject);
			_groupField.value = (Object) group;
			_groupField.SetEnabled(false);
			
			_textArea.value = group.Message;

			_fixBtn.clicked += Fix;
		}

		public void Fix()
		{
			List<IRestructure> undos = new();
			
			foreach (IRestructure restructure in _group.Restructures)
			{
				undos.Add(restructure);
				restructure.Do(_go);
			}
			
			ConsoleViewPresenter.UndoStack.Push(new Tuple<IRestructure[], GameObject>(undos.ToArray(), _go));
		}

		public object GetData()
		{
			return _group;
		}

		public void Destroy()
		{
			DestroyWithoutNotification();
			OnDestroy?.Invoke();
			OnDestroyThis?.Invoke(this);
		}

		public void DestroyWithoutNotification()
		{
			_root.Remove(_el);
		}
	}
}