using System;
using System.Collections.Generic;

namespace HierarchyOrganizer.Editor.Common
{
    [Serializable]
    public class HierarchyTree
    {
        public List<HierarchyNode> NodesStructure;
        public bool IsRelative;

        public HierarchyTree(bool isRelative) : this(new List<HierarchyNode>(), isRelative){}
        
        public HierarchyTree(List<HierarchyNode> nodesStructure, bool isRelative)
        {
            NodesStructure = nodesStructure;
            IsRelative = isRelative;
        }
    }
}