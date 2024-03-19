using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace RevitDev.XYZInRevits
{
    public static class XYZInRevit
    {
        public static bool IsPointInPolygon(this XYZ point, List<XYZ> polygon)
        {
            var plgCount = polygon.Count;
            bool result = false;
            int j = plgCount - 1;
            for (int i = 0; i < plgCount; i++)
            {
                if (polygon[i].Y < point.Y && polygon[j].Y >= point.Y || polygon[j].Y < point.Y && polygon[i].Y >= point.Y)
                {
                    if (polygon[i].X + (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < point.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }

        /// <summary>
        /// IsCounterClockWise() => [true, fasle]
        /// true = chieu duong (nguoc chieu kim dong ho)
        /// false = chieu am (cung chieu kim dong ho)
        /// </summary>
        /// <param name="polygons"></param>
        /// <returns></returns>
        public static bool IsCounterClockWise(this List<XYZ> polygons)
        {
            bool r = false;
            double sum = 0;
            for (int i = 0; i < polygons.Count - 1; i++)
            {
                var x1 = polygons[i].X;
                var x2 = polygons[i + 1].X;
                var y1 = polygons[i].Y;
                var y2 = polygons[i + 1].Y;
                sum += (x2 - x1) * (y2 + y1);
            }
            var sp = polygons[0];
            var ep = polygons[polygons.Count - 1];
            sum += (sp.X - ep.X) * (sp.Y + ep.Y);
            if (sum < 0)
            {
                r = true;
            }
            return r;
        }

        public static bool IsSeem(this XYZ p1, XYZ p2)
        {
            return (p1.X == p2.X && p1.Y == p2.Y && p1.Z == p2.Z);
        }

        public static List<Curve> GetCurves(this List<XYZ> points)
        {
            var pointsCount = points.Count;
            var results = new List<Curve>();
            if (pointsCount > 1)
            {
                for (int i = 0; i < pointsCount; i++)
                {
                    if (i == pointsCount - 1)
                    {
                        results.Add(Line.CreateBound(points[i], points[0]));

                    }
                    else
                    {
                        results.Add(Line.CreateBound(points[i], points[i + 1]));
                    }
                }
            }
            return results;
        }
    }
}
