using Node;
using RedBlackTree;
using System;
using System.Collections.Generic;
using System.Text;


class Test
{
    static void Main(String[] Args)
    {
        RedBlackTree<int> tree = new RedBlackTree<int>();

        tree.insert(7);
        tree.insert(3);
        tree.insert(18);
        tree.insert(10);
        tree.insert(22);
        tree.insert(8);
        tree.insert(11);
        tree.insert(26);
        tree.insert(2);
        tree.insert(6);
        tree.insert(13);

        tree.print();
        Console.WriteLine("-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-");

        tree.delete(18);
        tree.delete(11);
        tree.delete(3);
        tree.delete(10);
        tree.delete(22);

        tree.print();
    }

}

