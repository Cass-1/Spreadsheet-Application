namespace hw1;

public class Node
{
    // Atributes
    
    // the left child node
    public Node? Left { get; set; } = null;
    
    // the right child node
    public Node? Right { get; set; } = null;
    
    // the number the node contains
    public int Number { get; set; }

    // The Constructor
    public Node(int num = Int32.MinValue)
    {
        this.Number = num;
    }

}