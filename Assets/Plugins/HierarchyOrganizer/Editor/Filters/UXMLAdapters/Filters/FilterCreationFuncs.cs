using HierarchyOrganizer.Editor.Interfaces.Filters;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Filters.UXMLAdapters
{
	public partial class FiltersViewBuilderAdapter
	{
		private void AddTagFilter(ScrollView view) => AddFilter(view, new TagFilterElementAdapter());

		private void AddNameFilter(ScrollView view) => AddFilter(view, new NameFilterElementAdapter());

        private void AddComponentFilter(ScrollView view) => AddFilter(view, new ComponentFilterElementAdapter());

        private void AddFilter(ScrollView view, ISceneFilterElementAdapter adapter)
        {
	        adapter.Init(view);
	        _addedFilters.Add(adapter);
	        adapter.OnDelete += DeleteFilterFromList;
        }

        private void DeleteFilterFromList(ISceneFilterElementAdapter elementAdapter) => _addedFilters.Remove(elementAdapter);
	}
}