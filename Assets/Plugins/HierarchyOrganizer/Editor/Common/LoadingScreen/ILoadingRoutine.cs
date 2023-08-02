using System;
using System.Collections;

namespace HierarchyOrganizer.Editor.Common.LoadingScreen
{
	public interface ILoadingRoutine
	{
		public event Action OnFinished;
	
		/// <summary>
		/// Range => 0.0 - 1.0
		/// </summary>
		public event Action<float> OnProgress;
		
		public IEnumerator Routine { get; }
	}
}