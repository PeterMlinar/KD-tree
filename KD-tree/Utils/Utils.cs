using KD_tree.KDTree;
using KD_tree.ListData;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace KD_tree
{
    static class Utils
    {
        /// <summary>
        /// calculates distance between two tree nodes
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double CalculateDistance(Node a, Node b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        /// <summary>
        /// calculates distance between two points
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static double CalculateDistance(DPoint point1, DPoint point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }

        /// <summary>
        /// Converts k-d tree node to DPoint.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static DPoint FromNodeToDPoint(Node node)
        {
            DPoint res = new DPoint();
            if (node != null)
                res = new DPoint(node.X, node.Y);

            return res;
        }

        /// <summary>
        /// Compares two points, if both components are the same returns true.
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static bool ComparePoints(DPoint point1, DPoint point2)
        {
            if (point1.X == point2.X && point1.Y == point2.Y)
                return true;
            else return false;
        }

        #region Balancing points for tree
        /// <summary>
        /// Prepare balanced nodes for construction balanced node.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static List<DPoint> PrepareBalancedPoints(List<DPoint> points)
        {
            List<DPoint> result = new List<DPoint>();
            Node dp = GetNextPoint(points, 0, result);

            return result;
        }

        private static Node GetNextPoint(List<DPoint> points, int depth, List<DPoint> result)
        {
            if (points.Count == 0)
                return null;

            List<DPoint> sorted = new List<DPoint>(points);
            SortPoints(sorted, depth);

            //get median value
            int median = ((int)Math.Ceiling(sorted.Count / 2.0)) - 1;
            DPoint medianValue = sorted[median];
            Node node = new Node(medianValue.X, medianValue.Y);
            //System.Diagnostics.Debug.WriteLine("{0},{1}", medianValue.X, medianValue.Y);
            result.Add(medianValue);

            node.Left = GetNextPoint(sorted.GetRange(0, median), depth + 1, result);
            node.Right = GetNextPoint(sorted.GetRange(median + 1, sorted.Count - 1 - median), depth + 1, result);

            return node;
        }

        private static void SortPoints(List<DPoint> points, int depth)
        {
            if (depth % 2 == 0)
            {
                points.Sort(delegate(DPoint first, DPoint second)
                {
                    if (first.X < second.X)
                        return -1;
                    else if (first.X > second.X)
                        return 1;
                    else return 0;
                });
            }
            else
            {
                points.Sort(delegate(DPoint first, DPoint second)
                {
                    if (first.Y < second.Y)
                        return -1;
                    else if (first.Y > second.Y)
                        return 1;
                    else return 0;
                });
            }
        }
        #endregion
    }
}
