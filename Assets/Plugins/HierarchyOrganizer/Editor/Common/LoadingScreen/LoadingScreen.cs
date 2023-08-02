using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Common.LoadingScreen
{
	public class LoadingScreen
	{
		private const string UXML_PATH =
			"Assets/Plugins/HierarchyOrganizer/Editor/Common/LoadingScreen/UXML/LoadingScreenView.uxml";

		public LoadingScreen(VisualElement root, EditorWindow owner, ILoadingRoutine coroutine)
		{
			TemplateContainer el = AddUXML(coroutine);
			coroutine.OnFinished += () => root.Remove(el);
			owner.StartCoroutine(coroutine.Routine);
		}

		private TemplateContainer AddUXML(ILoadingRoutine routine)
		{
			TemplateContainer el = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH).Instantiate();
			ProgressBar progress = el.Q<ProgressBar>();
			progress.value = 0;
			routine.OnProgress += (val) => progress.value = val;
			return el;
		}
	}
}