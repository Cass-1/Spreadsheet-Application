namespace hw1;

public class BST
{
    // Attributes
    
    // the root node
    private Node? root { get; set; } = null;
    
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
            Console.Write(curr.Number + " ");
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
            Console.Write(curr.Number + " ");
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
            Console.Write(curr.Number + " ");
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
    
    // find levels helper
    private int levels_helper(Node? curr)
    {
        if (curr == null)
        {
            return 0;
        }

        int leftLevels = levels_helper(curr.Left);
        int rightLevels = levels_helper(curr.Right);

        if (leftLevels > rightLevels)
        {
            return leftLevels + 1;
        }
        else
        {
            return rightLevels + 1;
        }
    }
    
    // PUBLIC METHODS ------------------------------------------------------------------------


    // insert a node
    public void Insert(int num)
    {
        if (this.root == null)
        {
            this.root = insert_helper(num, this.root);
        }
        else
        {
            insert_helper(num, this.root);
        }
    }
    
    // preorder traversal
    public void PreorderTraversal()
    {
        preorder_helper(this.root);
    }

    // inorder traversal
    public void Inorder_Traversal()
    {
        inorder_helper(this.root);
    }

    // postorder traversal
    public void Postorder_Traversal()
    {
        postorder_helper(this.root);
    }
    
    // returns the number of nodes in the tree
    public int Count()
    {
        int count = 0;
        return count_helper(this.root, count);
    }

    // returns the number of levels in the tree
    public int Levels()
    {
        return levels_helper(this.root);
        return levels_helper(this.root);
    }

    // returns the minimum number of levels a tree with
    // this.Count() nodes can have
    public double TheoreticalMinLevels()
    {
        int n = this.Count();
        return Math.Ceiling(Math.Log2(n + 1));
    }
}