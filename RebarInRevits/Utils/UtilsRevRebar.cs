using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.DB;
using System.Collections.Generic;
using System;
using RevitDev.Utils;
using HcBimUtils.RebarUtils;
using Nice3point.Revit.Extensions;
using System.Linq;

namespace RevitDev.RebarInRevits.Utils
{
    public static class UtilsRevRebar
    {
        public static void CreateRebarBaseOldRebar(this Rebar oldRebar, Document document, List<Curve> newCurves)
        {
            XYZ rebarNormalNew = oldRebar.GetNormal();
            var rebarStyle = oldRebar.GetRebarStyle();
            var rebarType = oldRebar.GetRebarBarType();
            RebarHookType startHookType = null;
            RebarHookType endHookType = null;
            var host = document.GetElement(oldRebar.GetHostId());
            //var plane = Plane.CreateByThreePoints(newCurves[0].GetEndPoint(0), newCurves[0].GetEndPoint(1), newCurves[1].GetEndPoint(1));
            if (oldRebar.IsRebarShapeDriven() == false && rebarNormalNew == null) throw new Exception("");
            var vtNorm = oldRebar.IsRebarShapeDriven()
                ? oldRebar.GetShapeDrivenAccessor().Normal
                : rebarNormalNew;
            var startHookOrien = oldRebar.GetHookOrientation(0);
            var EndHookOrien = oldRebar.GetHookOrientation(1);
            Rebar rebarNew = null;
            using (var ts = new Transaction(document, "Create new rebar to flip"))
            {
                ts.SkipOutSiteHost();
                ts.Start();
                rebarNew = Rebar.CreateFromCurves(
                    document,
                    rebarStyle,
                    rebarType,
                    startHookType,
                    endHookType,
                    host,
                    vtNorm,
                    newCurves,
                    startHookOrien,
                    EndHookOrien,
                    true,
                    true);
                rebarNew.SetSolidInView(document.ActiveView as View3D, true);
                document.Delete(oldRebar.Id);
                ts.Commit();
            }
        }

        public static Rebar CopyRebar(this Rebar rebarBase, Document document, XYZ dir, double spacing)
        {
            Rebar rebar = null;
            using (var ts = new Transaction(document, "CopyRebar"))
            {
                ts.SkipOutSiteHost();
                ts.Start();
                rebar = document.GetElement(rebarBase.Copy(dir * spacing).FirstOrDefault()) as Rebar;
                ts.Commit();
            }
            return rebar;
        }

        public static void RotateRebar(this Rebar rebar, Document document, XYZ center, XYZ dir, double angleRad)
        {
            using (var ts = new Transaction(document, "RotateRebar"))
            {
                ts.SkipOutSiteHost();
                ts.Start();
                //--------
                ElementTransformUtils.RotateElement(document, rebar.Id, Line.CreateBound(center, center + dir * 1), angleRad);
                //--------
                ts.Commit();
            }
        }

        public static void SetRebarSolid(this Rebar rebar, Document document)
        {
            using (var ts = new Transaction(document, "name transaction"))
            {
                ts.Start();
                //--------
                if (document.ActiveView is View3D view3d)
                {
                    rebar.SetSolidInView(view3d, true);
                }
                ts.Commit();
            }
        }

        public static int GetQTYRebar(this double length, double spacing, out double phandu)
        {
            phandu = length % spacing;
            var qty = length != 0
                ? int.Parse(Math.Round((length - phandu) / spacing + 1, 0).ToString())
                : 0;
            return qty;
        }
    }
}
