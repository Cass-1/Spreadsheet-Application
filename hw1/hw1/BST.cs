namespace hw1;

public class BST
{
    // Attributes
    public Node? Root { get; set; } = null;
    
    // PRIVATE METHODS ------------------------------------------------------------------------

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
            return insert_helper(num, curr.Left);
        }
        // insert on the right
        else if (curr.Number > num)
        {
            return insert_helper(num, curr.Right);
        }
        // duplicate
        else
        {
            return false;
        }
    }

    // preorder traversal helper
    private void preorder_helper(Node? curr)
    {
        if (curr != null)
        {
            Console.WriteLine(curr.Number + " ");
            preorder_helper(curr.Left);
            preorder_helper(curr.Right);
        }
    }
    
    // inorder traversal helper
    private void inorder_helper(Node? curr)
    {
        if (curr != null)
        {
            inorder_helper(curr.Left);
            Console.WriteLine(curr.Number + " ");
            inorder_helper(curr.Right);
        }
    }
    
    // postorder traversal helper
    private void postorder_helper(Node? curr)
    {
        if (curr != null)
        {
            postorder_helper(curr.Left);
            postorder_helper(curr.Right);
            Console.WriteLine(curr.Number + " ");
        }
    }
    
    // PUBLIC METHODS ------------------------------------------------------------------------


    // insert a node
    public bool Insert(int num)
    {
        return insert_helper(num, this.Root);
    }
    
    // preorder traversal
    public void PreorderTraversal()
    {
        preorder_helper(this.Root);
    }

    // inorder traversal
    public void Inorder_Traversal()
    {
        inorder_helper(this.Root);
    }

    public void Postorder_Traversal()
    {
        postorder_helper(this.Root);
    }
}