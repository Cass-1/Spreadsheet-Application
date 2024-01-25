namespace hw1;

public class BST
{
    // Attributes
    public Node? Root { get; set; } = null;
    
    // PRIVATE METHODS ------------------------------------------------------------------------
    
    // creates a new node
    private Node newNode(int num)
    {
        Node node = new Node(num);
        return node;

    }
    // insert recursive helper
    private Node insert_helper(int num, Node? curr)
    {
        if (curr == null)
        {
            return newNode(num);
        }

        // insert on the left
        if (curr.Number > num)
        {
            curr.Left = insert_helper(num, curr.Left);
        }
        // insert on the right
        else if (curr.Number < num)
        {
            curr.Right = insert_helper(num, curr.Right);
        }
        
        
        return curr;
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
    
    // count helper
    private int count_helper(Node? curr, int count)
    {
        if (curr == null)
        {
            return 0;
        }

        return 1 + count_helper(curr.Left, count) + count_helper(curr.Right, count);
    }
    
    // PUBLIC METHODS ------------------------------------------------------------------------


    // insert a node
    public void Insert(int num)
    {
        if (this.Root == null)
        {
            this.Root = insert_helper(num, this.Root);
        }
        else
        {
            insert_helper(num, this.Root);
        }
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

    // postorder traversal
    public void Postorder_Traversal()
    {
        postorder_helper(this.Root);
    }

    //TODO: count is not working
    public int Count()
    {
        int count = 0;
        return count_helper(this.Root, count);
    }
}