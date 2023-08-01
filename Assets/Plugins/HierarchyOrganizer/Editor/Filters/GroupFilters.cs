namespace HierarchyOrganizer.Editor.Filters
{
	public struct GroupFilters
	{
		private readonly FilterBase[] _includeFilters;
		private readonly FilterBase[] _excludeFilters;

		public GroupFilters(FilterBase[] includeFilters, FilterBase[] excludeFilters)
		{
			_includeFilters = includeFilters;
			_excludeFilters = excludeFilters;
		}
	}
}