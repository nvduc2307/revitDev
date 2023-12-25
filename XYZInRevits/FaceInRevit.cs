using Autodesk.Revit.DB;
using Nice3point.Revit.Extensions;
using System;
using System.Collections.Generic;

namespace RevitDev.XYZInRevits
{
    public static class FaceInRevit
    {
        public static bool IsFace(this List<XYZ> points)
        {
            var result = false;
            if (points.Count > 2)
            {
                var p1 = points[0];
                var p2 = points[1];
                var p3 = points[2];

                if (p1.IsSeem(p2)) return false;
                if (p1.IsSeem(p3)) return false;
                if (p2.IsSeem(p3)) return false;

                points.ForEach(p => {
                    var distance = p.DistancePointToFace(points);
                    if (!distance.IsAlmostEqual(0)) return;
                });
                result = true;
            }
            return result;
        }
        public static double DistancePointToFace(this XYZ p, List<XYZ> polygon)
        {
            var p1 = polygon[0];
            var p2 = polygon[1];
            var p3 = polygon[2];

            var vt1 = (p2 - p1).Normalize();
            var vt2 = (p3 - p1).Normalize();
            var normal = vt1.CrossProduct(vt2);

            var vtCheck = (p1 - p).Normalize();

            normal = normal.DotProduct(vtCheck) > 0 
                ? normal 
                : -normal;
            var angle = normal.AngleTo(vtCheck);

            return Math.Cos(angle) * p.DistanceTo(p1);
        }
    }
}
