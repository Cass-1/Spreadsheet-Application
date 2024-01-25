namespace hw1;

public class Node
{
    public Node? Left { get; set; } = null;
    public Node? Right { get; set; } = null;
    public int Number { get; set; }

    public Node(int num = Int32.MinValue)
    {
        this.Number = num;
    }

}