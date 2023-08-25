using System;

namespace HierarchyOrganizer.Editor.Common
{
    [Serializable]
    public class HierarchyNode
    {
        public string Name;

        public HierarchyNode(string name)
        {
            Name = name;
        }
    }
}