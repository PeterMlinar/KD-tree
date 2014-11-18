using System;
using System.Collections.Generic;

namespace KD_tree.KDTree
{
    public class KDTree
    {
        public Node root;

        private Node currentNBest = null;
        private Node prevNode = null;

        private List<Node> treeNodes;
         
        public KDTree() 
        {
            treeNodes = new List<Node>();
        }

        private void ResetVisited()
        {
            foreach(Node node  in treeNodes)
            {
                node.Visited = false;
            }
        }

        public void ResetAll()
        {
            ResetVisited();
            currentNBest = null;
            prevNode = null;
        }

        public Node Add(Node newNode)
        {
            treeNodes.Add(newNode);

            //TODO this must be inserted only first time no checked all the time
            if (root == null)
            {
                root = newNode;
                return root;
            }

            return InsertNewNode(root, newNode, 0);
        }

        private Node InsertNewNode(Node currentNode, Node newNode, int depth)
        {
            //find parent to insert child
            Node node = GetLeaf(currentNode, newNode);

            if (node.Dimension)
            {
                if (newNode.X <= node.X)
                {
                    newNode.Type = NodeType.Left;
                    node.Left = newNode;
                }
                else
                {
                    newNode.Type = NodeType.Right;
                    node.Right = newNode;
                }
            }
            else
            {
                if (newNode.Y <= node.Y)
                {
                    newNode.Type = NodeType.Left;
                    node.Left = newNode;
                }
                else
                {
                    newNode.Type = NodeType.Right;
                    node.Right = newNode;
                }
            }

            newNode.Parent = node;
            newNode.Dimension = !node.Dimension;

            return node;
        }

        public Node FindNN(Node currentNode, Node newNode, double currentDistance)//, ref int epsilonUpdated)
        {
            //return if root is reached
            if (currentNode == null)
                return currentNBest;

            if (prevNode == null)
            {
                prevNode = newNode;
                prevNode.Visited = true;
            }

            if (currentNode.Visited)
            {
                prevNode = currentNode;
                return FindNN(currentNode.Parent, newNode, currentDistance);//, ref epsilonUpdated);
            }

            double distance = Utils.CalculateDistance(currentNode, newNode);
            if (distance < currentDistance)
            {
                currentDistance = distance;
                currentNBest = currentNode;
                //System.Diagnostics.Debug.WriteLine("best distance updated > {0}, {1} -> dist. {2}", currentNode.X, currentNode.Y, currentDistance);
                //epsilonUpdated++;
            }

            //debug output for testing
            //System.Diagnostics.Debug.WriteLine("node visit > {0}, {1} -> dist. {2}", currentNode.X, currentNode.Y, currentDistance);

            currentNode.Visited = true;

            double inspectValue;
            double newValue;

            if (currentNode.Dimension)
            {
                inspectValue = currentNode.X;
                newValue = newNode.X;
            }
            else
            {
                inspectValue = currentNode.Y;
                newValue = newNode.Y;
            }

            //if distance is closer than minimum inspect other side
            if (Math.Abs(inspectValue - newValue) < currentDistance)
            {
                Node child = GetChild(prevNode, currentNode);
                if (child != null && !child.Visited)
                {
                    Node leaf = GetSubtreeLeaf(child, newNode, currentDistance);

                    prevNode = currentNode;
                    return FindNN(leaf, newNode, currentDistance);//, ref epsilonUpdated);
                }
            }

            prevNode = currentNode;

            //other side of current best is avtomaticaly pruned
            return FindNN(currentNode.Parent, newNode, currentDistance);//, ref epsilonUpdated);
        }

        private Node GetChild(Node previous, Node current)
        {
            if (current.Left == null || current.Left == previous)
                return current.Right;
            else if (current.Right == null || current.Right == previous)
                return current.Left;

            return null;
        }

        private Node GetSubtreeLeaf(Node currentNode, Node newNode, double currentDistance)
        {
            double currentValue = 0;
            double newValue = 0;

            if (currentNode.Dimension)
            {
                currentValue = currentNode.X;
                newValue = newNode.X;
            }
            else
            {
                currentValue = currentNode.Y;
                newValue = newNode.Y;
            }

            if (newValue <= currentValue)
            {
                if (currentNode.Left == null)
                    return currentNode;
                else
                    return GetSubtreeLeaf(currentNode.Left, newNode, currentDistance);
            }
            else if (newValue > currentValue)
            {
                if (currentNode.Right == null)
                    return currentNode;
                else
                    return GetSubtreeLeaf(currentNode.Right, newNode, currentDistance);
            }

            return null;
        }

        public Node GetLeaf(Node currentNode, Node newNode)
        {
            double currentValue = 0;
            double newValue = 0;

            if (currentNode.Dimension)
            {
                currentValue = currentNode.X;
                newValue = newNode.X;
            }
            else
            {
                currentValue = currentNode.Y;
                newValue = newNode.Y;
            }

            if (newValue <= currentValue)
            {
                if (currentNode.Left == null)
                    return currentNode;
                else
                    return GetLeaf(currentNode.Left, newNode);
            }
            else
            {
                if (currentNode.Right == null)
                    return currentNode;
                else
                    return GetLeaf(currentNode.Right, newNode);
            }
        }

        public void ExportStructure()
        {
            System.Diagnostics.Debug.WriteLine("root ^ {0},{1}", root.X, root.Y);
            System.Diagnostics.Debug.WriteLine("{0},{1} <- root left || root right -> {2},{3}", root.Left.X, root.Left.Y, root.Right.X, root.Right.Y);
        }

        public int LeftDepth(Node node)
        {
            int st = 0;
            while(node.Left != null)
            {
                if (node.Left != null)
                {
                    node = node.Left;
                    st++;
                }
            }

            return st;
        }

        public int RightDepth(Node node)
        {
            int st = 0;
            while (node.Right != null)
            {
                if (node.Right != null)
                {
                    node = node.Right;
                    st++;
                }
            }

            return st;
        }
    }
}
