using System.Collections.Generic;
using System.Drawing;

namespace KD_tree.ListData
{
    public class ListData
    {
        List<DPoint> points;

        public ListData(List<DPoint> points)
        {
            this.points = points;
        }

        public ListData()
        {
            points = new List<DPoint>();
        }

        public DPoint IncrementalSearchNN(List<DPoint> points)
        {
            DPoint nn = new DPoint();

            foreach(DPoint point in points)
            {
                nn = FindNearestPoint(point);
            }

            return nn;
        }

        public DPoint FindNearestPoint(DPoint newPoint)
        {
            DPoint nn = new DPoint();

            if (points.Count > 1)
            {
                nn = SearchNN(newPoint);
                System.Diagnostics.Debug.WriteLine("NN -> {0} {1}", nn.X, nn.Y);
            }

            points.Add(newPoint);

            return nn;
        }

        public DPoint SearchNN(DPoint newPoint)
        {
            DPoint bestPoint = points[0];
            double minDist = 9999999999;

            foreach (DPoint point in points)
            {
                double distance = Utils.CalculateDistance(newPoint, point);

                if (distance < minDist)
                {
                    minDist = distance;
                    bestPoint = point;
                }
            }

            return bestPoint;
        }
    }
}
