using KD_tree.KDTree;
using KD_tree.ListData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace KD_tree
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.White;
        }

        private void DrawPoints(List<DPoint> points)
        {
            System.Drawing.Graphics graphics = this.CreateGraphics();
            graphics.Clear(Color.White);

            //get bounding box
            double maxY = points.Max(e => e.Y);
            double minY = points.Min(e => e.Y);
            double maxX = points.Min(e => e.X);
            double minX = points.Min(e => e.X);

            //left top margin
            double addX = 10;
            double addY = 10;

            double multiplier = 1;
            double windowSize = 300;

            if (minX <= 0)
                addX = addX + Math.Abs(minX);
            else addX = addX - minX;

            if (minY <= 0)
                addY = addY + Math.Abs(minY);
            else addY = addY - minY;

            //scale point range to screen
            if (maxY >= maxX)
                multiplier = windowSize / (maxY);
            else multiplier = windowSize / (maxX);


            foreach (DPoint p in points)
            {
                graphics.FillRectangle(System.Drawing.Brushes.Black, ((float)p.X * (float)multiplier) * 1000 + (float)addX, ((float)p.Y * (float)multiplier) + (float)addY, 1, 1);
            }
        }

        private void DrawStats(string title, DPoint nnPoint, DPoint newPoint, int textPositionY, TimeSpan elapsed)
        {
            double minDist = Utils.CalculateDistance(nnPoint, newPoint);

            System.Drawing.Graphics graphics = this.CreateGraphics();

            graphics.DrawString(title, new System.Drawing.Font("Arial", 10), System.Drawing.Brushes.Black, 50, textPositionY);
            graphics.DrawString(String.Format("Best point: ({0},{1})", nnPoint.X, nnPoint.Y, minDist), new System.Drawing.Font("Arial", 10), System.Drawing.Brushes.Black, 50, textPositionY + 15);
            graphics.DrawString(String.Format("Distance: {0}", minDist), new System.Drawing.Font("Arial", 10), System.Drawing.Brushes.Black, 50, textPositionY + 30);
            graphics.DrawString(String.Format("SearchTime: {0} ms", elapsed.TotalMilliseconds), new System.Drawing.Font("Arial", 10), System.Drawing.Brushes.Black, 50, textPositionY + 45);
        }

        private void ClearForm()
        {
            System.Drawing.Graphics graphics = this.CreateGraphics();
            graphics.Clear(Color.White);
        }

        /// <summary>
        /// Linear searching.
        /// </summary>
        /// <param name="newPoint"></param>
        /// <param name="points"></param>
        /// <param name="showStats"></param>
        /// <returns></returns>
        private DPoint LinearSearchWithStats(DPoint newPoint, List<DPoint> points, bool showStats)
        {
            //linear search
            DPoint result = new DPoint();
            ListData.ListData listData = new ListData.ListData(points);
            result = listData.SearchNN(newPoint);
             
            if(showStats)
                DrawStats("Linear search", result, newPoint, 80, new TimeSpan(0));

            return result;
        }

        /// <summary>
        /// Searching NN only in first path with k-d tree.
        /// </summary>
        /// <param name="newPoint"></param>
        /// <param name="points"></param>
        /// <param name="showStats"></param>
        /// <returns></returns>
        private DPoint KdSearchFirstPathWithStats(DPoint newPoint, List<DPoint> points, bool showStats, bool balanced)
        {
            //searching with k-d tree
            KDTreeData kdData = new KDTreeData();

            if (balanced)
                kdData.ConstructBalancedKdTree(points);
            else kdData.ConstructKdTree(points);

            DPoint nnPoint = kdData.ReturnFirstParent(newPoint);

            //int lDepth = kdData.TreeLeftDepth();
            //int rDepth = kdData.TreeRightDepth();

            //System.Diagnostics.Debug.WriteLine("{0},{1}", lDepth, rDepth);
            
            if(showStats)
                DrawStats("KDtree", nnPoint, newPoint, 160, new TimeSpan(0));

            return nnPoint;
        }

        /// <summary>
        /// Searching NN with k-d tree.
        /// </summary>
        /// <param name="newPoint"></param>
        /// <param name="points"></param>
        /// <param name="showStats"></param>
        /// <returns></returns>
        private DPoint KdSearchWithStats(DPoint newPoint, List<DPoint> points, bool showStats, bool balanced)
        {
            //searching with k-d tree
            KDTreeData kdData = new KDTreeData();

            //if (balanced)
            //    kdData.ConstructBalancedKdTree(points);
            //else 

            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            kdData.ConstructKdTree(points);
            watch.Stop();            

            //DPoint nnPoint = kdData.SearchNN(newPoint);

            //int lDepth = kdData.TreeLeftDepth();
            //int rDepth = kdData.TreeRightDepth();

            //System.Diagnostics.Debug.WriteLine("{0},{1}", lDepth, rDepth);

            //if (showStats)
            //    DrawStats("KDtree", nnPoint, newPoint, 80, watch.Elapsed);

            Console.WriteLine(String.Format("{0}", watch.Elapsed.TotalMilliseconds));

            watch.Reset();
            

            return null;
        }

        /// <summary>
        /// Used for testing left and right depth with median or random points.
        /// </summary>
        /// <param name="newPoint"></param>
        /// <param name="points"></param>
        /// <param name="withMedian"></param>
        private void KdTreeDepthOnPoints(List<DPoint> points, bool withMedian)
        {
            //construct k-d tree
            KDTreeData kdData = new KDTreeData();

            if (withMedian)
                kdData.ConstructBalancedKdTree(points);
            else kdData.ConstructKdTree(points);

            int lDepth = kdData.TreeLeftDepth();
            int rDepth = kdData.TreeRightDepth();

            System.Diagnostics.Debug.WriteLine("{0},{1}", lDepth, rDepth);
        }

        /// <summary>
        /// Used for testing first path finding on space size !!!
        /// </summary>
        private void KdTreeFirstDepthTestCase()
        {
            int st = 0;
            Random random = new Random();

            for (int j = 0; j < 101; j++)
            {
                st = 0;

                List<DPoint> points = DataGenerators.DataGenerator.GenerateRandomPoints(100);

                //for (int i = 0; i < 100; i++)
                //{
                    DPoint newPoint = new DPoint((double)random.Next(0, 100), (double)random.Next(0, 100));
                    if (Utils.ComparePoints(LinearSearchWithStats(newPoint, points, false), KdSearchFirstPathWithStats(newPoint, points, false, true)))
                        st++;
                //}

                System.Diagnostics.Debug.WriteLine("{0}", st);
            }
        }        

        /// <summary>
        /// Incremental naive search test with stopwatch
        /// </summary>
        private void IncrementalNaiveSearchFunctionsNN()
        {
            //prepare points from file
            List<DPoint> points = DataGenerators.DataGenerator.ReadPointsFromFile("ledder1000000wDistPoint");
            List<DPoint> balancedPoints = Utils.PrepareBalancedPoints(points);

            ListData.ListData listData = new ListData.ListData();

            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

            watch.Start();
            DPoint nnPoint = listData.IncrementalSearchNN(balancedPoints);
            watch.Stop();

            DrawStats("Naive search", nnPoint, new DPoint(0, 0), 160, watch.Elapsed);
        }

        /// <summary>
        /// First points are prepared and incremental tree is constucted and test with stopwatch is performed
        /// </summary>
        private void ConstructIncrementalKdTreeAndSearchNN()
        {
            //prepare points from file
            List<DPoint> points = DataGenerators.DataGenerator.GenerateRandomPoints(10000);

            //construct k-d tree
            KDTreeData kdData = new KDTreeData();
            List<DPoint> balancedPoints = Utils.PrepareBalancedPoints(points);

            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

            watch.Start();
            DPoint nnPoint = kdData.IncrementalNNSearch(balancedPoints);
            watch.Stop();

            //DrawStats("KDtree", nnPoint, new DPoint(0,0), 80, watch.Elapsed);
            Console.WriteLine(String.Format("{0}", watch.Elapsed.TotalMilliseconds));
        }
        
        private void drawPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearForm();

            ConstructIncrementalKdTreeAndSearchNN();
        }
    }
}
