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
            Node<T> v = this.search(item);
            if (v == null)
                return;
            this.delete(v);
        }
        private void delete(Node<T> v)
        {
            Node<T> u = bst_replace(v);
            bool vu_black = v.Color == COLOR.BLACK && (u == null || u.Color == COLOR.BLACK);
            if (u == null)
            {
                if (v.Val.CompareTo(this._root.Val) == 0)
                    this._root = null;
                else
                {
                    if (vu_black)
                        fix_double_black(v);
                    else
                    {
                        if (v.sibling() != null)
                        {
                            v.sibling().Color = COLOR.RED;
                        }
                    }
                    if (v.on_right())
                        v.Parent.Right = null;
                    else
                        v.Parent.Left = null;
                }
                return;
            }
            if (v.Left == null || v.Right == null)
            {
                if (v == this._root)
                {
                    v.Val = u.Val;
                    v.Left = null;
                    v.Right = null;
                }
                else
                {
                    if (v.on_right())
                        v.Parent.Right = u;
                    else
                        v.Parent.Left = u;
                    u.Parent = v.Parent;
                    if (vu_black)
                        fix_double_black(u);
                    else
                        u.Color = COLOR.BLACK;
                }
                return;
            }
            this.swap_values(u, v);
            this.delete(u);
        }

        private void fix_double_black(Node<T> v)
        {
            if (v == this._root)
                return;
            Node<T> sibling = v.sibling(), parent = v.Parent;
            if (sibling == null)
                fix_double_black(parent);
            else
            {
                if (sibling.Color == COLOR.RED)
                {
                    parent.Color = COLOR.RED;
                    sibling.Color = COLOR.BLACK;
                    if (sibling.on_right())
                        left_rotate(parent);
                    else
                        right_rotate(parent);
                    fix_double_black(v);
                }
                else
                {
                    if (sibling.has_red_child())
                    {
                        if (sibling.Right != null && sibling.Right.Color == COLOR.RED)
                        {
                            if (sibling.on_right())
                            {
                                sibling.Right.Color = sibling.Color;
                                sibling.Color = parent.Color;
                                left_rotate(parent);
                            }
                            else
                            {
                                sibling.Right.Color = parent.Color;
                                left_rotate(sibling);
                                right_rotate(parent);
                            }
                        }
                        else
                        {
                            if (sibling.on_right())
                            {
                                sibling.Left.Color = parent.Color;
                                right_rotate(sibling);
                                left_rotate(parent);
                            }
                            else
                            {
                                sibling.Left.Color = sibling.Color;
                                sibling.Color = parent.Color;
                                right_rotate(parent);
                            }
                        }
                        parent.Color = COLOR.BLACK;
                    }
                    else
                    {
                        sibling.Color = COLOR.RED;
                        if (parent.Color == COLOR.BLACK)
                            fix_double_black(parent);
                        else
                            parent.Color = COLOR.BLACK;
                    }
                }
            }
        }

        private Node<T> bst_replace(Node <T> n)
        {
            if (n.Left != null && n.Right != null)
                return this.successosr(n.Left);
            if (n.Left == null && n.Right == null)
                return null;
            if (n.Right == null)
                return n.Right;
            else
                return n.Left;
        }

        private Node<T> successosr(Node <T> n)
        {
            while (n.Right != null)
                n = n.Right;
            return n;
        }

        private void swap_values(Node<T> n1, Node<T> n2)
        {
            T temp = n1.Val;
            n1.Val = n2.Val;
            n2.Val = temp;
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
