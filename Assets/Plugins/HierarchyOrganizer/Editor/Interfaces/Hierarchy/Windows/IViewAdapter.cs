namespace HierarchyOrganizer.Editor.Interfaces.Hierarchy.Windows
{
	public interface IViewAdapter : IViewPresenter
	{
		public object GetData();
		public void DestroyWithoutNotification();
	}
}