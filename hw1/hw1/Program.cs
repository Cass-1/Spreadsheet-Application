// See https://aka.ms/new-console-template for more information

// read in the line of numbers from the console
string numberLine = Console.ReadLine();

// seperate each of the numbers on the line and put them in an array
string[] nums = numberLine.Split(' ');

// for every number add it to the BST
foreach (var num in nums)
{
    // TODO: add the numbers to the BST
    Console.WriteLine($"num: {num}");
}
