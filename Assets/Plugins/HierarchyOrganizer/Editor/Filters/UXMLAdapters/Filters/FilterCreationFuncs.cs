using HierarchyOrganizer.Editor.Interfaces.Filters;
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
        private void AddNameFilter(ScrollView view)
        {
            NameFilterElementAdapter elementAdapter = new NameFilterElementAdapter();
            elementAdapter.Init(view);
            _addedFilters.Add(elementAdapter);
            elementAdapter.OnDelete += DeleteFilterFromList;
        }
<<<<<<< HEAD
        private void AddComponentFilter(ScrollView view)
        {
            ComponentFilterElementAdapter elementAdapter = new ComponentFilterElementAdapter();
            elementAdapter.Init(view);
            _addedFilters.Add(elementAdapter);
            elementAdapter.OnDelete += DeleteFilterFromList;
        }
=======
>>>>>>> 1a6d8003e48946a629d8b989ce86bdff5d745a81

        private void DeleteFilterFromList(ISceneFilterElementAdapter elementAdapter) => _addedFilters.Remove(elementAdapter);
	}
}