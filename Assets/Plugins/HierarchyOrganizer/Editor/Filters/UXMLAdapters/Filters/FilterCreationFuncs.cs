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

        private void AddComponentFilter(ScrollView view)
        {
            ComponentFilterElementAdapter elementAdapter = new ComponentFilterElementAdapter();
            elementAdapter.Init(view);
            _addedFilters.Add(elementAdapter);
            elementAdapter.OnDelete += DeleteFilterFromList;
        }


>>>>>>> 8e98faf4c32fc8385df620cce116a1f0141358e8
        private void DeleteFilterFromList(ISceneFilterElementAdapter elementAdapter) => _addedFilters.Remove(elementAdapter);
	}
}