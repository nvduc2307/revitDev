using Autodesk.Revit.DB;
using HcBimUtils;
using System.Collections.Generic;

namespace RevitDev.Solids
{
    public static class SolidUtils
    {
        public static Solid CreateSolidVertical(this Document document, List<XYZ> polygons, double heightSolid)
        {
            // create new solid with min elevation = -1000.
            Solid result = null;
            var polygonsCount = polygons.Count;
            if (polygonsCount > 2)
            {
                //create list curveloop
                var curveLoop = new CurveLoop();
                for (int i = 0; i < polygonsCount; i++)
                {
                    if (i != polygonsCount - 1)
                    {
                        var p1 = new XYZ(polygons[i].X, polygons[i].Y, -1000.MmToFoot());
                        var p2 = new XYZ(polygons[i + 1].X, polygons[i + 1].Y, -1000.MmToFoot());
                        curveLoop.Append(Line.CreateBound(p1, p2));
                    }
                    else
                    {
                        var p1 = new XYZ(polygons[i].X, polygons[i].Y, -1000.MmToFoot());
                        var p2 = new XYZ(polygons[0].X, polygons[0].Y, -1000.MmToFoot());
                        curveLoop.Append(Line.CreateBound(p1, p2));
                    }
                }
                //create solid
                result = GeometryCreationUtilities.CreateExtrusionGeometry(new List<CurveLoop>() { curveLoop }, XYZ.BasisZ, heightSolid.MmToFoot());
            }
            return result;
        }
    }
}
