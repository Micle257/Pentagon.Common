// -----------------------------------------------------------------------
//  <copyright file="HierarchyList.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Collections.Tree
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using JetBrains.Annotations;

    /// <summary> Represents a collection in hierarchy structure. </summary>
    /// <typeparam name="T"> The type of an item. </typeparam>
    public class HierarchyList<T> : IEnumerable<HierarchyListNode<T>>
    {
        /// <summary>
        /// Gets all children nodes.
        /// </summary>
        /// <returns>Iteration of <see cref="HierarchyListNode{T}"/>.</returns>
        IEnumerable<HierarchyListNode<T>> GetAllNodes()
        {
            yield return Root;

            foreach (var node in Root.GetDescendants())
            {
                yield return node;
            }
        }

        /// <summary> Initializes a new instance of the <see cref="HierarchyList{T}" /> class. </summary>
        /// <param name="rootItem"> The root item. </param>
        public HierarchyList(T rootItem)
        {
            Root = new HierarchyListNode<T>(rootItem, null, 0);
        }

        /// <summary> Gets the root item. </summary>
        /// <value> The list of the <see cref="HierarchyListNode{T}" />. </value>
        [NotNull]
        public HierarchyListNode<T> Root { get; }

        /// <summary>
        /// Gets the number of nodes in tree.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public int Size => this.Count();

        /// <inheritdoc />
        public IEnumerator<HierarchyListNode<T>> GetEnumerator() => GetAllNodes().GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public string ToTreeString(string depthText = "-")
        {
            return ToTreeString(Builder);

            string Builder(int depth) => string.Join("", Enumerable.Repeat(depthText, depth));
        }

        public string ToTreeString([NotNull] Func<int, string> depthTextBuilder)
        {
            var sb = new StringBuilder();

            foreach (var item in this)
            {
                sb.Append(depthTextBuilder(item.Depth));
                sb.Append(item.Value);
                sb.AppendLine();
            }

            return sb.ToString();
        }

        [Pure]
        [NotNull]
        public static IEnumerable<HierarchyList<T>> FromDictionaryFreely<TSub>([NotNull] IReadOnlyDictionary<T, TSub> inputMap)
                where TSub : IEnumerable<T>
        {
            var determinedRoot = inputMap.Select(a => a.Key).Except(inputMap.SelectMany(a => a.Value)).ToList();

            foreach (var x1 in determinedRoot)
            {
                yield return FromDictionaryFreely(x1, inputMap);
            }
        }

        [Pure]
        [NotNull]
        public static HierarchyList<T> FromDictionaryFreely<TSub>(T root, [NotNull] IReadOnlyDictionary<T, TSub> inputMap)
                where TSub : IEnumerable<T>
        {
            var cachedMap = inputMap.ToDictionary(a => a.Key, a => a.Value);

            var hierarchy = new HierarchyList<T>(root);

            InitializeHierarchy(hierarchy.Root, cachedMap);

            return hierarchy;

            void InitializeHierarchy(HierarchyListNode<T> node, IDictionary<T, TSub> map)
            {
                if (map.TryGetValue(node.Value, out var subItems))
                {
                    map.Remove(node.Value);

                    foreach (var subCommandInfo in subItems)
                    {
                        var newNode = node.AddChildren(subCommandInfo);

                        InitializeHierarchy(newNode, map);
                    }
                }
            }
        }

        /// <summary>
        /// Creates a hierarchy list from dictionary, which specifies item relations.
        /// </summary>
        /// <typeparam name="TSub">The type of the sub item.</typeparam>
        /// <param name="root">The root.</param>
        /// <param name="inputMap">The map.</param>
        /// <returns>A new <see cref="HierarchyList{T}"/>.</returns>
        [Pure]
        [NotNull]
        public static HierarchyList<T> FromDictionary<TSub>([NotNull] IReadOnlyDictionary<T, TSub> inputMap, IEqualityComparer<T> comparer = null)
                where TSub : IEnumerable<T>
        {
            comparer ??= EqualityComparer<T>.Default;

            var cache = new HashSet<T>();

            var cachedMap = inputMap.ToDictionary(a => a.Key, a => a.Value);

            var determinedRoot = inputMap.Select(a => a.Key).Except(inputMap.SelectMany(a => a.Value)).ToList();

            if (determinedRoot.Count == 0)
                throw new ArgumentException("Root cannot be detected.");

            if (determinedRoot.Count > 1)
                throw new ArgumentException("Multiple roots found.");

            var root = determinedRoot[0];

            var hierarchy = new HierarchyList<T>(root);

            InitializeHierarchy(hierarchy.Root, cachedMap);

            return hierarchy;

            void InitializeHierarchy(HierarchyListNode<T> node, IDictionary<T, TSub> map)
            {
                if (cache.Contains(node.Value))
                {
                    if (node.Parent != null && node.Value.Equals(node.Parent.Value))
                    {
                        // self reference
                        throw new ArgumentException($"Self reference found for: {node.Value}");
                    }
                    else
                    {
                        var parents = inputMap.Where(a => a.Value.Any(b => b.Equals(node.Value)) || comparer.Equals(a.Key, root)).ToList();
                        // multiple parents
                        throw new ArgumentException($"Multiple parent nodes ({parents.Count()}) found for: {node.Value}\nParents: {string.Join(";", parents.Select(a => a.Key.ToString()))}");
                    }
                }

                cache.Add(node.Value);

                if (map.TryGetValue(node.Value, out var subItems))
                {
                    map.Remove(node.Value);

                    foreach (var subCommandInfo in subItems)
                    {
                        var newNode = node.AddChildren(subCommandInfo);

                        InitializeHierarchy(newNode, map);
                    }
                }
            }
        }
    }
}