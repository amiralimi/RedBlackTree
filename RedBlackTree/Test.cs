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
        rb.insert(10);
        rb.insert(20);
        rb.insert(30);
        rb.insert(15);
        rb.print();
    }

}

