// See https://aka.ms/new-console-template for more information

// print statement

using hw1;

Console.WriteLine("Input integer numbers [0,100] seperated by spaces");

// bst
BST bst = new BST();

// read in the line of numbers from the console
string numberLine = Console.ReadLine();

// seperate each of the numbers on the line and put them in an array
string[] nums = numberLine.Split(' ');

// for every number add it to the BST
foreach (var num in nums)
{
    bst.Insert(int.Parse(num));
}
Console.Write("\nTree contents: ");
bst.Inorder_Traversal();

Console.WriteLine("\n");
Console.WriteLine("Tree statistics: ");
Console.WriteLine("The tree has " + bst.Count() + " nodes");
Console.WriteLine("The tree has " + bst.Levels() + " levels\n");
Console.WriteLine("Minimum number of levels that a tree with " + bst.Levels() + " could have = " + bst.TheoreticalMinLevels());


