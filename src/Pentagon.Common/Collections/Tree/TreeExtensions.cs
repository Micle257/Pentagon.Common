namespace Pentagon.Collections.Tree {
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    public static class TreeExtensions
    {
        [NotNull]
        [Pure]
        public static IReadOnlyDictionary<T, IReadOnlyCollection<T>> ToDictionaryMap<T>([NotNull] this HierarchyList<T> tree)
        {
            return tree.Root.ToDictionaryMap();
        }

        [NotNull]
        [Pure]
        public static IEnumerable<HierarchyListNode<T>> GetBranchNodes<T>([NotNull] this HierarchyList<T> tree)
        {
            return tree.Where(a => TreeNodeExtensions.IsBranchNode<T>(a));
        }

        [NotNull]
        [Pure]
        public static IEnumerable<HierarchyListNode<T>> GetLeafNodes<T>([NotNull] this HierarchyList<T> tree)
        {
            return tree.Where(a => a.IsLeafNode());
        }

        [Pure]
        public static int GetDegree<T>([NotNull] this HierarchyList<T> tree) => tree.Max(a => a.GetNodeDegree());

        [Pure]
        public static int GetHeight<T>([NotNull] this HierarchyList<T> tree) => tree.Root.GetNodeHeight();

        [Pure]
        public static bool IsSubTreeOf<T>([NotNull] this HierarchyList<T> tree, [NotNull] HierarchyList<T> otherTree) => otherTree.Root.GetDescendants().Any(a => a == tree.Root);
    }
}