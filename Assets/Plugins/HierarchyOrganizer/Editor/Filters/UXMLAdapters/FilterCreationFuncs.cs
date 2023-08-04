using Plugins.HierarchyOrganizer.Editor.Interfaces.Filters;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Filters.UXMLAdapters
{
	public partial class FiltersBuilderViewAdapter
	{
		private void AddTagFilter(ScrollView view)
		{
			TagFilterAdapter adapter = new TagFilterAdapter();
			adapter.Init(view);
			_addedFilters.Add(adapter);
			adapter.OnDelete += DeleteFilterFromList;
		}

		private void DeleteFilterFromList(ISceneFilterAdapter adapter) => _addedFilters.Remove(adapter);
	}
}