using KD_tree.ListData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace KD_tree.DataGenerators
{
    static class DataGenerator
    {
        //test point must be 
        //Node newNode = new Node(90, 50);

        public static List<DPoint> ReadPointsFromFile(string file)
        {
            List<DPoint> points = new List<DPoint>(); 
            List<string> lines = System.IO.File.ReadAllLines(string.Format(@"..\..\..\Data\{0}.dat", file)).ToList();
            lines.RemoveAt(0);

            string[] value;

            foreach (string line in lines)
            {
                value = line.Split(' ');
                points.Add(new DPoint() { X = Convert.ToDouble(value[0], CultureInfo.InvariantCulture), 
                                          Y = Convert.ToDouble(value[1], CultureInfo.InvariantCulture) });
            }

            return points;
        }

        public static List<DPoint> GenerateRandomPoints(int pointsCount)
        {
            List<DPoint> points = new List<DPoint>(); 

            Random random = new Random();

            for (int i = 0; i < pointsCount; i++)
            {
                double x = (double)random.Next(0, 10000);
                double y = (double)random.Next(0, 10000);

                points.Add(new DPoint(x,y));
            }

            return points;
        }

        public static List<DPoint> Generate10PointsForKD()
        {
            List<DPoint> points = new List<DPoint>();
            points.Add(new DPoint(1, 10));
            points.Add(new DPoint(2, 20));
            points.Add(new DPoint(3, 30));
            points.Add(new DPoint(4, 40));
            points.Add(new DPoint(5, 50));
            points.Add(new DPoint(6, 60));
            points.Add(new DPoint(7, 70));
            points.Add(new DPoint(8, 80));
            points.Add(new DPoint(9, 90));
            points.Add(new DPoint(10, 100));

            return points;
        }

        public static List<DPoint> Generate10FailPointsForKD()
        {
            List<DPoint> points = new List<DPoint>();
            points.Add(new DPoint(4, 4));
            points.Add(new DPoint(3, 5));
            points.Add(new DPoint(3, 3));
            points.Add(new DPoint(0, 9));
            points.Add(new DPoint(3, 9));
            points.Add(new DPoint(7, 4));
            points.Add(new DPoint(5, 2));
            points.Add(new DPoint(5, 4));
            points.Add(new DPoint(5, 5));
            points.Add(new DPoint(5, 7));

            return points;
        }

        public static List<Point> GenerateTestPointsForKD()
        {
            List<Point> points = new List<Point>(); 
            points.Add(new Point(70, 20));
            points.Add(new Point(50, 40));
            points.Add(new Point(20, 30));
            points.Add(new Point(40, 70));
            points.Add(new Point(110, 60));
            points.Add(new Point(80, 10));
            points.Add(new Point(90, 10));
            points.Add(new Point(90, 70));            

            return points;
        }

        public static List<Point> GenerateTestPointsForKDCrash()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(683, 36));
            points.Add(new Point(590, 533));
            points.Add(new Point(531, 341));
            points.Add(new Point(54, 352));
            points.Add(new Point(116, 449));
            points.Add(new Point(198, 722));
            points.Add(new Point(662, 597));
            points.Add(new Point(705, 592));
            points.Add(new Point(698, 68));
            points.Add(new Point(727, 263));

            return points;
        }

        public static List<Point> GenerateTestPointsForKDMiss()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(677, 204));
            points.Add(new Point(222, 174));
            points.Add(new Point(112, 222));
            points.Add(new Point(371, 702));
            points.Add(new Point(701, 448));
            points.Add(new Point(512, 405));
            points.Add(new Point(220, 453));
            points.Add(new Point(133, 323));
            points.Add(new Point(389, 341));
            points.Add(new Point(237, 505));

            return points;
        }

        public static List<Point> GenerateTestPointsForKDMiss1()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(347, 241));
            points.Add(new Point(384, 452));
            points.Add(new Point(377, 310));
            points.Add(new Point(360, 4));
            points.Add(new Point(728, 47));
            points.Add(new Point(606, 266));
            points.Add(new Point(441, 634));
            points.Add(new Point(492, 569));
            points.Add(new Point(652, 541));
            points.Add(new Point(612, 729));

            return points;
        }

        public static List<Point> GenerateTestPointsForKDMiss2()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(190, 461));
            points.Add(new Point(41, 48));
            points.Add(new Point(70, 735));
            points.Add(new Point(63, 84));
            points.Add(new Point(562, 692));
            points.Add(new Point(539, 363));
            points.Add(new Point(480, 156));
            points.Add(new Point(325, 396));
            points.Add(new Point(570, 588));
            points.Add(new Point(613, 500));

            return points;
        }

        public static List<Point> GenerateTestPointsForKDMiss3()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(146, 721));
            points.Add(new Point(19, 720));
            points.Add(new Point(523, 105));
            points.Add(new Point(370, 289));
            points.Add(new Point(192, 577));
            points.Add(new Point(193, 278));
            points.Add(new Point(328, 711));
            points.Add(new Point(744, 699));
            points.Add(new Point(687, 223));
            points.Add(new Point(626, 513));

            return points;
        }

        public static List<Point> GenerateTestPointsForKDMiss4()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(48, 188));
            points.Add(new Point(267, 595));
            points.Add(new Point(702, 655));
            points.Add(new Point(703, 491));
            points.Add(new Point(78, 685));
            points.Add(new Point(154, 5));
            points.Add(new Point(245, 512));
            points.Add(new Point(140, 423));
            points.Add(new Point(165, 551));
            points.Add(new Point(243, 455));

            return points;
        }

        public static List<Point> GenerateTestPointsForKDMiss5()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(239, 343));
            points.Add(new Point(129, 427));
            points.Add(new Point(51, 459));
            points.Add(new Point(53, 685));
            points.Add(new Point(123, 660));
            points.Add(new Point(311, 476));
            points.Add(new Point(519, 394));
            points.Add(new Point(266, 268));
            points.Add(new Point(735, 387));
            points.Add(new Point(307, 651));

            return points;
        }

        public static List<Point> GenerateTestPointsForKDMiss6()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(607, 279));
            points.Add(new Point(596, 331));
            points.Add(new Point(244, 290));
            points.Add(new Point(152, 315));
            points.Add(new Point(36, 222));
            points.Add(new Point(578, 90));
            points.Add(new Point(490, 103));
            points.Add(new Point(487, 580));
            points.Add(new Point(256, 490));
            points.Add(new Point(83, 432));

            return points;
        }
    }
}
