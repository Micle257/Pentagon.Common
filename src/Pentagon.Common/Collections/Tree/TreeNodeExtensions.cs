namespace Pentagon.Collections.Tree {
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    public static class TreeNodeExtensions
    {
        /// <summary>
        /// Gets all children (includes children of children) nodes.
        /// </summary>
        /// <returns>Read-only list of nodes.</returns>
        [NotNull]
        [ItemNotNull]
        [Pure]
        public static IEnumerable<HierarchyListNode<T>> GetDescendants<T>([NotNull] this HierarchyListNode<T> node)
        {
            return GetChildrenRecursive(node.Children);

            static IEnumerable<HierarchyListNode<T>> GetChildrenRecursive(IReadOnlyList<HierarchyListNode<T>> children)
            {
                foreach (var node in children)
                {
                    yield return node;

                    var nodeChildren = node.Children;

                    foreach (var child in GetChildrenRecursive(nodeChildren))
                    {
                        yield return child;
                    }
                }
            }
        }

        /// <summary>
        /// Converts given node into new sub tree.
        /// </summary>
        /// <returns>A <see cref="HierarchyList{T}"/>.</returns>
        [NotNull]
        [Pure]
        public static HierarchyList<T> ToSubTree<T>([NotNull] this HierarchyListNode<T> node)
        {
            var map = node.ToDictionaryMap();

            return HierarchyList<T>.FromDictionary(map);
        }

        [NotNull]
        [Pure]
        public static IReadOnlyDictionary<T, IReadOnlyCollection<T>> ToDictionaryMap<T>([NotNull] this HierarchyListNode<T> node)
        {
            var result = new Dictionary<T, IReadOnlyCollection<T>>();

            foreach (var hierarchyListNode in node.GetBranchDescendants().Prepend(node))
            {
                result.Add(hierarchyListNode.Value, hierarchyListNode.Children.Select(a => a.Value).ToList().AsReadOnly());
            }

            return result;
        }

        [NotNull]
        [Pure]
        public static IEnumerable<HierarchyListNode<T>> GetBranchDescendants<T>([NotNull] this HierarchyListNode<T> node)
        {
            return node.GetDescendants().Where(a => a.IsBranchNode());
        }

        [NotNull]
        [Pure]
        public static IEnumerable<HierarchyListNode<T>> GetLeafDescendants<T>([NotNull] this HierarchyListNode<T> node)
        {
            return node.GetDescendants().Where(a => a.IsLeafNode());
        }

        [NotNull]
        [ItemNotNull]
        [Pure]
        public static IEnumerable<HierarchyListNode<T>> GetAncestors<T>([NotNull] this HierarchyListNode<T> node)
        {
            if (node.IsRoot)
                yield break;

            var parent = node.Parent;

            while (parent?.Parent != null)
            {
                yield return parent;

                parent = parent.Parent;
            }
        }

        [NotNull]
        [ItemNotNull]
        [Pure]
        public static IEnumerable<HierarchyListNode<T>> GetNeighbors<T>([NotNull] this HierarchyListNode<T> node)
        {
            if (node.IsRoot)
                return node.Children;

            return node.Children.Prepend(node.Parent);
        }

        [Pure]
        public static bool IsLeafNode<T>([NotNull] this HierarchyListNode<T> node) => node.Children.Count == 0;

        [Pure]
        public static bool IsBranchNode<T>([NotNull] this HierarchyListNode<T> node) => node.Children.Count > 0;

        [Pure]
        public static int GetNodeDegree<T>([NotNull] this HierarchyListNode<T> node) => node.Children.Count;

        [Pure]
        public static int GetNodeLevel<T>([NotNull] this HierarchyListNode<T> node) => node.Depth + 1;

        [Pure]
        public static int GetNodeHeight<T>([NotNull] this HierarchyListNode<T> node) => node.GetDescendants().Count();

        [Pure]
        public static HierarchyListNode<T> FindCommonParentNodeWith<T>([NotNull] this HierarchyListNode<T> node, [NotNull] HierarchyListNode<T> otherNode)
        {
            var ancestors      = node.GetAncestors().ToList();
            var otherAncestors = otherNode.GetAncestors().ToList();

            var bigger  = ancestors.Count >= otherAncestors.Count ? ancestors : otherAncestors;
            var smaller = ancestors.Count < otherAncestors.Count ? ancestors : otherAncestors;

            foreach (var hierarchyListNode in bigger)
            {
                if (smaller.Any(n => n == hierarchyListNode))
                {
                    return hierarchyListNode;
                }
            }

            return null;
        }

        [Pure]
        public static bool IsRelatedWith<T>([NotNull] this HierarchyListNode<T> node, [NotNull] HierarchyListNode<T> otherNode)
        {
            var root1 = otherNode.FindRoot();
            var root2 = node.FindRoot();

            return root2 == root1;
        }

        [Pure]
        public static bool IsDescendantOf<T>([NotNull] this HierarchyListNode<T> node, [NotNull] HierarchyListNode<T> otherNode)
        {
            return otherNode.GetDescendants().Any(a => a == node);
        }

        [Pure]
        public static bool IsChildOf<T>([NotNull] this HierarchyListNode<T> node, [NotNull] HierarchyListNode<T> otherNode)
        {
            return otherNode.Children.Any(a => a == node);
        }

        [Pure]
        public static bool IsAncestorOf<T>([NotNull] this HierarchyListNode<T> node, [NotNull] HierarchyListNode<T> otherNode)
        {
            return otherNode.GetAncestors().Any(a => a == node);
        }

        [Pure]
        public static bool IsParentOf<T>([NotNull] this HierarchyListNode<T> node, [NotNull] HierarchyListNode<T> otherNode)
        {
            if (node.IsRoot)
                return false;

            return otherNode == node.Parent;
        }

        [NotNull]
        [Pure]
        public static HierarchyListNode<T> FindRoot<T>([NotNull] this HierarchyListNode<T> node) => GetAncestors(node).LastOrDefault() ?? node;

        /// <summary> Gets the siblings. </summary>
        /// <returns> A list of the <see cref="HierarchyListNode{T}" />. </returns>
        [NotNull]
        [Pure]
        public static IEnumerable<HierarchyListNode<T>> GetSiblings<T>([NotNull] this HierarchyListNode<T> node)
        {
            if (node.IsRoot || node.Parent == null)
                yield break;

            if (node.Parent.Children.Count == 0)
                yield break;

            foreach (var n in node.Parent.Children.Except(new[] { node }))
                yield return n;
        }
    }
}