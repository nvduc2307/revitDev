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
    }
}
