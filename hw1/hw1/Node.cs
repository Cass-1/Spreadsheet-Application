namespace hw1;

public class Node
{
    public Node? Left { get; set; } = null;
    public Node? Right { get; set; } = null;
    public int Number { get; set; } = 0;

    public Node(int num)
    {
        this.Number = num;
    }

}