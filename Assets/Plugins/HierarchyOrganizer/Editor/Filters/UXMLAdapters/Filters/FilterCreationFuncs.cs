using Plugins.HierarchyOrganizer.Editor.Interfaces.Filters;
using UnityEngine.UIElements;

namespace HierarchyOrganizer.Editor.Filters.UXMLAdapters
{
	public partial class FiltersBuilderViewBuilderAdapter
	{
		private void AddTagFilter(ScrollView view)
		{
			TagFilterElementAdapter elementAdapter = new TagFilterElementAdapter();
			elementAdapter.Init(view);
			_addedFilters.Add(elementAdapter);
			elementAdapter.OnDelete += DeleteFilterFromList;
		}

		private void DeleteFilterFromList(ISceneFilterElementAdapter elementAdapter) => _addedFilters.Remove(elementAdapter);
	}
}