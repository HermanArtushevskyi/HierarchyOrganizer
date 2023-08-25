using System;
using System.Collections.Generic;

namespace HierarchyOrganizer.Editor.Common
{
    [Serializable]
    public class HierarchyTree
    {
        public HierarchyNode[] NodesStructure;
        public bool IsRelative;

        public HierarchyTree(bool isRelative) : this(new HierarchyNode[]{}, isRelative){}
        
        public HierarchyTree(HierarchyNode[] nodesStructure, bool isRelative)
        {
            NodesStructure = nodesStructure;
            IsRelative = isRelative;
        }

        public HierarchyTree(string[] names, bool isRelative)
        {
            List<HierarchyNode> nodes = new();
            foreach (string name in names) nodes.Add(new HierarchyNode(name));

            NodesStructure = nodes.ToArray();
            IsRelative = isRelative;
        }
    }
}