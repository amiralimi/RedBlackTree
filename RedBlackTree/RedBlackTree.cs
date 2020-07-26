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
            fix_red_red(new_item);
            this.print();
            Console.WriteLine("______________________________________________");
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

        private void fix_red_red(Node<T> x)
        {
            if (x.Val.CompareTo(this._root.Val) == 0)
            {
                x.Color = COLOR.BLACK;
                return;
            }
            Node<T> parent = x.Parent, grandfather = parent.Parent, uncle = x.GetUncle();
            if (parent.Color == COLOR.RED)
            {
                if (uncle != null && uncle.Color == COLOR.RED)
                {
                    parent.Color = COLOR.BLACK;
                    uncle.Color = COLOR.BLACK;
                    grandfather.Color = COLOR.RED;
                    fix_red_red(grandfather);
                }
                else
                {

                    if (parent.on_right())
                    {
                        if (x.on_right())
                        {
                            this.swap_colors(grandfather, parent);
                        }
                        else
                        {
                            this.right_rotate(parent);
                            this.swap_colors(grandfather, x);
                        }
                        this.left_rotate(grandfather);
                    }
                    else
                    {
                        if (!x.on_right())
                            this.swap_colors(grandfather, parent);
                        else
                        {
                            this.left_rotate(parent);
                            this.swap_colors(grandfather, x);
                        }
                        this.right_rotate(grandfather);
                    }

                }
            }
        }

        private void left_rotate(Node<T> r)
        {
            Node<T> new_parent = r.Right, temp = new_parent.Left;
            if (r == this._root)
                this._root = new_parent;
            r.move_down(new_parent);
            r.Right = temp;
            if (temp != null)
                temp.Parent = r;
            new_parent.Left = r;
        }

        private void right_rotate(Node<T> r)
        {
            Node<T> new_parent = r.Left, temp = new_parent.Right;
            if (r == this._root)
                this._root = new_parent;
            r.move_down(new_parent);
            r.Left = temp;
            if (temp != null)
                temp.Parent = r;
            new_parent.Right = r;
        }

        private void swap_colors(Node<T> a, Node<T> b)
        {
            COLOR temp = a.Color;
            a.Color = b.Color;
            b.Color = temp;
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
            this.rec_print(this._root);
        }

        private void rec_print(Node<T> root)
        {
            if (root == null)
                return;
            Console.WriteLine(root.ToString);
            this.rec_print(root.Left);
            this.rec_print(root.Right);
        }
    }
}
