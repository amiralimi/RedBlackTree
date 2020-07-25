using System;
using System.Collections.Generic;
using System.Text;

namespace Node
{
    enum COLOR { RED, BLACK }

    class Node<T> where T: IComparable
    {
        private T _val;
        private Node<T> _left, _right, _parent;
        private COLOR _color;

        public Node(T val)
        {
            this._val = val;
            _left = null;
            _right = null;
            _parent = null;
            _color = COLOR.RED;
        }

        public T Val
        {
            get => this._val; set => this._val = value;
        }

        public Node<T> Right
        {
            get => this._right; set => this._right = value;
        }

        public Node<T> Left
        {
            get => this._left; set => this._left = value;
        }

        public Node<T> Parent
        {
            get => this._parent; set => this._parent = value;
        }

        public COLOR Color
        {
            get => this._color; set => this._color = value;
        }

        public Node<T> GetUncle()
        {
            if (this._parent == null || this._parent._parent == null)
                return null;
            else if (this.on_right())
                return this._parent._parent._left;
            else
                return this._parent._parent._right;

        }

        public bool on_right()
        {
            return this == this._parent._right;
        }
    }

}