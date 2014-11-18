
namespace KD_tree.KDTree
{
    public enum NodeType { Left, Right };

    public class Node
    {
        public double X;
        public double Y;
        public Node Left;
        public Node Right;
        public Node Parent;
        public bool Dimension;
        public NodeType Type;
        public bool Visited;

        public Node()
        {

        }

        public Node(double x, double y)
        {
            X = x;
            Y = y;
            Left = null;
            Right = null;
            Dimension = true;
            Visited = false;
        }
    }
}
