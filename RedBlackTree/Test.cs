using Node;
using RedBlackTree;
using System;
using System.Collections.Generic;
using System.Text;


class Test
{
    static void Main(String[] Args)
    {
        RedBlackTree<int> rb = new RedBlackTree<int>();
        rb.insert(5);
        rb.insert(10);
        rb.print();
    }

}

