using System;
using System.Collections.Generic;
using HierarchyOrganizer.Editor.Common;
using HierarchyOrganizer.Editor.Hierarchy.Data;
using HierarchyOrganizer.Editor.Hierarchy.Windows.Common.ConsoleField;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy;
using HierarchyOrganizer.Editor.Interfaces.Hierarchy.Windows;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Hierarchy.Windows.ConsoleWindow
{
	public class ConsoleViewPresenter : IViewPresenter
	{
		private const string UXML_PATH =
			"Assets/Plugins/HierarchyOrganizer/Editor/Hierarchy/Windows/ConsoleWindow/UXML/ConsoleWindowView.uxml";

		private VisualElement _root;
		private TemplateContainer _el;
		private ScrollView _scrollView;

		private List<ConsoleFieldAdapter> _fields = new();
		
		public event Action OnDestroy;

		public static Stack<Tuple<IRestructure[], GameObject>> UndoStack = new();
		public static Stack<Tuple<IRestructure[], GameObject>> RedoStack = new();

		public void Init(VisualElement root)
		{
			_root = root;
			_el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH).Instantiate();

			_root.Add(_el);

			_el.Q<ToolbarButton>("undoBtn").clicked += Undo;
			_el.Q<ToolbarButton>("redoBtn").clicked += Redo;

			_el.Q<ToolbarButton>("fixBtn").clicked += FixAll;
			
			_el.Q<ToolbarButton>("refreshBtn").clicked += () =>
			{
				foreach (ConsoleFieldAdapter adapter in _fields)
				{
					adapter.DestroyWithoutNotification();
				}
				
				_fields.Clear();
				
				ProcessCurrentScene();
			};

			_scrollView = _el.Q<ScrollView>();
			
			ProcessCurrentScene();
		}

		private void Undo()
		{
			if (UndoStack.Count <= 0) return;
			
			Tuple<IRestructure[], GameObject> undo = UndoStack.Pop();

			List<IRestructure> redo = new();

			foreach (IRestructure restructure in undo.Item1)
			{
				redo.Add(restructure);
				restructure.Undo(undo.Item2);
			}

			RedoStack.Push(new Tuple<IRestructure[], GameObject>(redo.ToArray(), undo.Item2));
		}

		private static void Redo()
		{
			if (RedoStack.Count <= 0) return;

			Tuple<IRestructure[], GameObject> redo = RedoStack.Pop();

			List<IRestructure> undo = new();

			foreach (IRestructure restructure in redo.Item1)
			{
				undo.Add(restructure);
				restructure.Do(redo.Item2);
			}

			UndoStack.Push(new Tuple<IRestructure[], GameObject>(undo.ToArray(), redo.Item2));
		}

		private void FixAll()
		{
			foreach (ConsoleFieldAdapter adapter in _fields)
			{
				adapter.Fix();
			}
		}

		public void Destroy()
		{
			_root.Remove(_el);
			OnDestroy?.Invoke();
		}

		private void ProcessCurrentScene()
		{
			GameObject[] allObjects = HierarchySceneUtils.GetAllObjectsOnScene(SceneManager.GetActiveScene());

			ProcessGlobalGroups(allObjects);
		}

		private void ProcessGlobalGroups(GameObject[] gameObjects)
		{
			IGroup[] globalGroups = GlobalGroupsData.GetAllGlobalGroups();

			foreach (GameObject go in gameObjects)
			{
				foreach (IGroup group in globalGroups)
				{
					if (GlobalGroupsData.ObjectMeetsRequirements(go, group))
						AddAdapter(_scrollView, group, go);
				}
			}
		}

		private void AddAdapter(VisualElement body, IGroup group, GameObject go)
		{
			ConsoleFieldAdapter adapter = new ConsoleFieldAdapter();
			adapter.Init(body, group, go);
			_fields.Add(adapter);
			adapter.OnDestroyThis += ProcessAdapterDeletion;
		}

		private void ProcessAdapterDeletion(ConsoleFieldAdapter obj) => _fields.Remove(obj);
	}
}