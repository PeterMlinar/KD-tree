using KD_tree.ListData;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace KD_tree.KDTree
{
    public class KDTreeData
    {
        KDTree tree;

        public KDTreeData()
        {
            tree = new KDTree();
        }

        public void ConstructKdTree(List<DPoint> points)
        {
            tree.root = new Node(points[0].X, points[0].Y);

            foreach(DPoint p in points.GetRange(1, points.Count-1))
            {
                tree.Add(new Node(p.X, p.Y));
            }
        }

        #region BalancedKdTree
        public void ConstructBalancedKdTree(List<DPoint> points)
        {
            List<DPoint> result = Utils.PrepareBalancedPoints(points);
            ConstructKdTree(result);
        }
        #endregion

        public DPoint IncrementalNNSearch(List<DPoint> points)
        {
            DPoint nn = new DPoint();

            foreach(DPoint p in points)
            {
                nn = SearchNN(p);
                System.Diagnostics.Debug.WriteLine("NN -> {0} {1}", nn.X, nn.Y);
            }

            return nn;
        }

        public DPoint SearchNN(DPoint point)
        {
            //use for incremental search
            tree.ResetAll();

            Node newNode = new Node(point.X, point.Y);
            tree.Add(newNode);

            //int epsilonUpdated = 0;

            //find nn in tree
            Node nnNode = tree.FindNN(newNode.Parent, newNode, double.MaxValue);//, ref epsilonUpdated);

            //System.Diagnostics.Debug.WriteLine("{0}", epsilonUpdated-1);

            DPoint res = new DPoint();
            if(nnNode != null)
                res = new DPoint(nnNode.X, nnNode.Y);

            return res;
        }

        /// <summary>
        /// Used for testing first nearest point stats !
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public DPoint ReturnFirstParent(DPoint point)
        {
            Node newNode = new Node(point.X, point.Y);
            tree.Add(newNode);
            return Utils.FromNodeToDPoint(newNode.Parent);
        }


        /// <summary>
        /// Getting left subtree depth;
        /// </summary>
        /// <returns></returns>
        public int TreeLeftDepth()
        {
            return tree.LeftDepth(tree.root);
        }

        /// <summary>
        /// Getting right subtree depth.
        /// </summary>
        /// <returns></returns>
        public int TreeRightDepth()
        {
            return tree.RightDepth(tree.root);
        }
    }
}
