using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

namespace HierarchyOrganizer.Editor.Common
{
	public static class HierarchyProjectUtils
	{
		private static bool TypesDirty = true;
		private static bool NamesDirty = true;

		private static Type[] _typesCache = new Type[]{};
		private static string[] _namesCache = new string[]{};
		private static Dictionary<string, Type> _monobehsCache = new();

		public static Type[] GetAllMonoBehavioursInProjectAsync()
		{
			if (!TypesDirty) return _typesCache;
			
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

			ConcurrentBag<Type> list = new ConcurrentBag<Type>();
			Parallel.ForEach(assemblies, assembly =>
			{
				Type[] allTypes = assembly.GetTypes();
				foreach (Type type in allTypes)
				{
					if (!type.IsClass) continue;
					if (type.IsAbstract) continue;
					if (type.IsSubclassOf(typeof(Component)))
					{
						list.Add(type);
					}
				}
			});

			var res = list.ToArray();

			TypesDirty = false;
			_typesCache = res;
			return res;
		}
		
		public static string[] GetAllMonoBehavioursNamesInProjectAsync()
		{
			if (!NamesDirty) return _namesCache;

			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

			ConcurrentBag<string> list = new ConcurrentBag<string>();
			Parallel.ForEach(assemblies, assembly =>
			{
				Type[] allTypes = assembly.GetTypes();
				foreach (Type type in allTypes)
				{
					if (!type.IsClass) continue;
					if (type.IsAbstract) continue;
					if (type.IsSubclassOf(typeof(Component)))
					{
						list.Add(type.Name);
					}
				}
			});
			
			var res = list.ToArray();

			NamesDirty = false;
			_namesCache = res;
			return res;
		}

		public static Type GetMonoBehaviourByName(string name)
		{
			if (_monobehsCache.ContainsKey(name)) return _monobehsCache[name];
			
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

			Type resType = null;
			
			Parallel.ForEach(assemblies, (assembly, state) =>
			{
				Type[] allTypes = assembly.GetTypes();
				foreach (Type type in allTypes)
				{
					if (!type.IsClass) continue;
					if (type.IsAbstract) continue;
					if (type.IsSubclassOf(typeof(Component)) && type.Name == name)
					{
						resType = type;
						state.Break();
					}
				}
			});
			
			_monobehsCache.Add(name, resType);
			
			return resType;
		}
	}
}