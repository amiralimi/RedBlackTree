using System;
using System.Collections;
using Node;

namespace RedBlackTree
{
    interface Tree<T> where T : IComparable
    {
        void insert(T item);
        Node<T> search(T item);
        void delete(T item);
    }

    class RedBlackTree<T> : Tree<T> where T : IComparable
    {
        private Node<T> _root;
        public void delete(T item)
        {
            throw new NotImplementedException();
        }

        public void insert(T item)
        {
            Node<T> new_item = new Node<T>(item);
            if (this._root == null)
            {
                new_item.Color = COLOR.BLACK;
                this._root = new_item;
                return;
            }
            this.bst_insert(_root, new_item);
        }

        private Node<T> bst_insert(Node<T> root, Node<T> new_item)
        {
            if (root == null)
            {
                root = new_item;
                return root;
            }
            if (root.Val.CompareTo(new_item.Val) > 0)
            {
                root.Left = bst_insert(root.Left, new_item);
                root.Left.Parent = root;
            }
            else if (root.Val.CompareTo(new_item.Val) < 0)
            {
                root.Right = bst_insert(root.Right, new_item);
                root.Right.Parent = root;
            }
            return root;
        }

        public Node<T> search(T item)
        {
            Node<T> temp = _root;
            while (temp != null)
            {
                if (temp.Val.CompareTo(item) == 0)
                    break;
                if (temp.Val.CompareTo(item) > 0)
                    temp = temp.Left;
                else
                    temp = temp.Right;
            }
            return temp;
        }

        public void print()
        {
            Console.WriteLine(this._root.Val);
            Console.WriteLine(this._root.Right.Val);
        }
    }
}
