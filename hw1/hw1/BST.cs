namespace hw1;

public class BST
{
    public Node? Root { get; set; } = null;
    
    // insert recursive helper
    private bool insert_helper(int num, Node? curr)
    {
        if (curr == null)
        {
            curr = new Node(num);
            return true;
        }

        // insert on the left
        if (curr.Number < num)
        {
            insert_helper(num, curr.Left);
        }
        // insert on the right
        else if (curr.Number > num)
        {
            insert_helper(num, curr.Right);
        }
        // duplicate
        else
        {
            return false;
        }
    }

    // insert a node
    public bool Insert(int num)
    {
        return insert_helper(num, this.Root);
    }
    
    // preorder traversal
    public void Preorder(Node? curr)
    {
        if (curr != null)
        {
            Console.WriteLine(curr.Number + " ");
            Preorder(curr.Left);
            Preorder(curr.Right);
        }
    }
    
    // inorder traversal
    public void Inorder(Node? curr)
    {
        if (curr != null)
        {
            Inorder(curr.Left);
            Console.WriteLine(curr.Number + " ");
            Inorder(curr.Right);
        }
    }
}