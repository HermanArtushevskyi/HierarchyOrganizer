using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Common
{
	public static class HierarchyProjectUtils
	{
		public static Type[] GetAllMonoBehavioursInProjectAsync()
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

			ConcurrentBag<Type> list = new ConcurrentBag<Type>();
			Parallel.ForEach(assemblies, assembly =>
			{
				Type[] allTypes = assembly.GetTypes();
				foreach (Type type in allTypes)
				{
					if (!type.IsClass) continue;
					if (type.IsSubclassOf(typeof(Component)))
					{
						list.Add(type);
					}
				}
			});
			return list.ToArray();
		}
		
		public static string[] GetAllMonoBehavioursNamesInProjectAsync()
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

			ConcurrentBag<string> list = new ConcurrentBag<string>();
			Parallel.ForEach(assemblies, assembly =>
			{
				Type[] allTypes = assembly.GetTypes();
				foreach (Type type in allTypes)
				{
					if (!type.IsClass) continue;
					if (type.IsSubclassOf(typeof(Component)))
					{
						list.Add(type.Name);
					}
				}
			});
			return list.ToArray();
		}

		public static Type GetMonoBehaviourByName(string name)
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

			Type resType = null;
			
			Parallel.ForEach(assemblies, (assembly, state) =>
			{
				Type[] allTypes = assembly.GetTypes();
				foreach (Type type in allTypes)
				{
					if (!type.IsClass) continue;
					if (type.IsSubclassOf(typeof(Component)) && type.Name == name)
					{
						resType = type;
						state.Break();
					}
				}
			});

			return resType;
		}
	}
}