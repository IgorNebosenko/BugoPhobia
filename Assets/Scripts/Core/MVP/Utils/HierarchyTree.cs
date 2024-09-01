#if !ELECTRUM_CORE
using System.Collections;
using System.Collections.Generic;

namespace ElectrumGames.MVP.Utils
{
    internal interface IReadonlyHierarchyTree<out T> : IEnumerable<T>
    {
        T Root { get; }
    }

    internal class HierarchyTree<T> : IReadonlyHierarchyTree<T> where T : class
    {
        private struct TreeNodeData
        {
            public T parent;
            public List<T> children;

            public TreeNodeData(T parent)
            {
                this.parent = parent;
                children = new List<T>();
            }
        }

        private Dictionary<T, TreeNodeData> _nodes = new Dictionary<T, TreeNodeData>();

        private T _root;
        
        public T Root => _root;

        public void RemoveItem(T item)
        {
            var data = _nodes[item];
            if (data.parent != null) _nodes[data.parent].children.Remove(item);
            foreach (var child in data.children)
                RemoveItem(child);


            _nodes.Remove(item);

            if (_root == item) _root = null;
        }

        public void SetRoot(T newRoot)
        {
            _root = newRoot;
            _nodes.Clear();
            _nodes.Add(newRoot, new TreeNodeData(null));
        }

        public void AddItem(T item, T under)
        {
            _nodes[under].children.Add(item);
            _nodes[item] = new TreeNodeData(under);
        }

        public IReadOnlyList<T> GetSubItemsOf(T item)
        {
            return _nodes[item].children;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _nodes.Keys.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return _nodes.Keys.GetEnumerator();
        }
    }
}
#endif